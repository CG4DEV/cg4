using System.Text;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace CG4.Impl.Kafka.Consumer
{
    public class KafkaConsumer : IKafkaConsumer, IDisposable
    {
        private class StringDeserializer : IDeserializer<string>
        {
            public string Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
            {
                return Encoding.UTF8.GetString(data.ToArray());
            }
        }

        private const int NO_ROLLBACK = 0;

        private const int INVALID_PARTITION = -1;

        private const int VALID_PARTITION = 0;

        private const int MAX_ROLLBACK_IN_MINUTES = 60 * 24 * 7;

        private readonly string _topic;
        private readonly int _maxTimeoutMsec;
        private readonly IConsumer<string, string> _consumer;
        private readonly int _topicPartitionToAssign;
        private readonly ILogger<KafkaConsumer> _logger;
        private readonly CancellationTokenSource _stopCancellationTokenSource = new CancellationTokenSource();

        private Task _pollTask;
        private int _startCount;
        private bool _isSubscribed;
        private bool _isAssigned;
        private bool _isPartitionEofReachedOnce;

        public event AsyncEventHandler<MessageReceivedEventArgs> MessageReceived;

        public event EventHandler ConsumerInitialized;

        public event EventHandler KafkaError;

        private readonly SemaphoreSlim _semaphore;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, IKafkaConsumerSettings settings, string topic)
        {
            _logger = logger;
            _topic = topic;
            _maxTimeoutMsec = settings.MaxTimeoutMsec;

            _consumer = new ConsumerBuilder<string, string>(settings.Config)
                .SetKeyDeserializer(new StringDeserializer())
                .SetValueDeserializer(new StringDeserializer())
                .SetLogHandler(Consumer_OnLog)
                .SetErrorHandler(Consumer_OnError).Build();

            _topicPartitionToAssign = GetTopicPartition(settings.Config);

            _semaphore = new SemaphoreSlim(settings.MaxThreadsCount, settings.MaxThreadsCount);
        }

        public void Dispose()
        {
            Stop();
            _consumer?.Dispose();
        }

        public void Start()
        {
            StartWithRollback(NO_ROLLBACK);
        }

        public void StartWithRollback(int rollbackInMinutes)
        {
            var needStart = Interlocked.Increment(ref _startCount) == 1;
            if (!needStart)
            {
                return;
            }

            var startWithoutRollback = rollbackInMinutes <= NO_ROLLBACK;

            if (startWithoutRollback)
            {
                Subscribe();
            }
            else
            {
                AssignWithRollback(rollbackInMinutes);
            }

            _pollTask = Task.Factory.StartNew(PollLoop, TaskCreationOptions.LongRunning);
        }

        public void Stop()
        {
            _stopCancellationTokenSource.Cancel();

            if (_pollTask != null && !_pollTask.Wait(_maxTimeoutMsec))
            {
                _logger.LogWarning("Poll task stopping timed out");
            }

            if (_consumer == null)
            {
                return;
            }

            if (_isSubscribed)
            {
                _consumer.Unsubscribe();
            }

            if (_isAssigned)
            {
                _consumer.Unassign();
            }
        }

        private void Subscribe()
        {
            _logger.LogDebug("Start");
            _consumer.Subscribe(_topic);
            _isSubscribed = true;
            _logger.LogDebug("Consumer is subscribed");
        }

        private void AssignWithRollback(int rollbackInMinutes)
        {
            if (rollbackInMinutes > MAX_ROLLBACK_IN_MINUTES)
            {
                _logger.LogDebug($"Rollback indicated {rollbackInMinutes} is too high. Set rollback value to {MAX_ROLLBACK_IN_MINUTES} minutes.");
                rollbackInMinutes = MAX_ROLLBACK_IN_MINUTES;
            }

            _logger.LogDebug($"Start with rollback {rollbackInMinutes} minutes");

            if (_topicPartitionToAssign == INVALID_PARTITION)
            {
                throw new KafkaConsumerException("Failed to start Kafka consumer");
            }

            var offsetWithRollback = GetTopicOffsetByTimeRollback(rollbackInMinutes, _topicPartitionToAssign);
            if (offsetWithRollback == Offset.Unset)
            {
                throw new KafkaConsumerException("Failed to start Kafka consumer");
            }

            var topicPartition = new TopicPartition(_topic, _topicPartitionToAssign);
            var topicPartitionOffset = new TopicPartitionOffset(topicPartition, offsetWithRollback);
            _consumer.Assign(new List<TopicPartitionOffset> { topicPartitionOffset });
            _isAssigned = true;

            _logger.LogDebug("Consumer is assigned");
        }

        private async Task PollLoop()
        {
            while (!_stopCancellationTokenSource.IsCancellationRequested && _consumer != null)
            {
                await _semaphore.WaitAsync();

                try
                {
                    var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(_maxTimeoutMsec));
                    if (consumeResult == null)
                    {
                        _semaphore.Release();
                    }
                    else if (consumeResult.IsPartitionEOF)
                    {
                        if (!_isPartitionEofReachedOnce)
                        {
                            _isPartitionEofReachedOnce = true;
                            ProcessPartitionEof(consumeResult);
                        }

                        _semaphore.Release();
                    }
                    else
                    {
                        HandleAsync(consumeResult);
                    }
                }
                catch (KafkaException e)
                {
                    _logger.LogError($"Error occured while Poll.\r\n{e}");
                    KafkaError?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception e)
                {
                    _logger.LogError($"Error occured while Poll.\r\n{e}");
                }
            }
        }

        private void ProcessPartitionEof(ConsumeResult<string, string> message)
        {
            _logger.LogDebug($"Topic {_topic} partition {message.Partition.Value} reached EOF with offset {message.Offset.Value}. Raising ConsumerActualized event");

            try
            {
                ConsumerInitialized?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Error while raising {nameof(ConsumerInitialized)} event\r\n{exception}");
            }
        }

        private async Task HandleAsync(ConsumeResult<string, string> consumeResult)
        {
            try
            {
                if (consumeResult == null)
                {
                    _logger.LogError("Consumed message is empty");
                    return;
                }

                var data = consumeResult.Message.Value;
                var key = consumeResult.Message.Key;

                var argument = new MessageReceivedEventArgs(consumeResult.Partition.Value, consumeResult.Offset.Value, key, data, consumeResult.Message.Timestamp.UtcDateTime);
                await MessageReceived?.InvokeAsync(this, argument);
            }
            catch (Exception exc)
            {
                _logger.LogError($"[{_topic}] Message with offset={consumeResult?.Offset.Value ?? Offset.Unset} handling failed!\r\n{exc}");
            }
            finally
            {
                try
                {
                    _consumer.Commit(consumeResult);
                }
                finally
                {
                    _semaphore.Release();
                }
            }
        }

        private void Consumer_OnLog(object sender, LogMessage e)
        {
            _logger.LogDebug($"OnLog {e.Facility}|{e.Level}|{e.Name}|{e.Message}");
        }

        private void Consumer_OnError(object sender, Error e)
        {
            _logger.LogError(e.ToString());
        }

        private int GetTopicPartition(Dictionary<string, string> options)
        {
            var adminClientBuilder = new AdminClientBuilder(options).SetErrorHandler(Consumer_OnError).SetLogHandler(Consumer_OnLog);
            var adminClient = adminClientBuilder.Build();
            var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));

            if (metadata == null)
            {
                _logger.LogError("GetMetadata returned null");
                return INVALID_PARTITION;
            }

            var topicMetadata = metadata.Topics.FirstOrDefault(topic => topic.Topic == _topic);
            var partitions = topicMetadata?.Partitions;

            if (partitions == null || !partitions.Any())
            {
                _logger.LogError($"Topic {_topic} has no partitions");
                return INVALID_PARTITION;
            }

            var topicPartitionIds = partitions.Select(item => item.PartitionId).ToArray();

            if (!topicPartitionIds.Contains(VALID_PARTITION))
            {
                _logger.LogError($"Topic {_topic} has no partition with id '{VALID_PARTITION}'. Partitions: {string.Join(",", topicPartitionIds)}");
                return INVALID_PARTITION;
            }

            return VALID_PARTITION;
        }

        private long GetTopicOffsetByTimeRollback(int rollbackInMinutes, int topicPartition)
        {
            var dateWithRollback = DateTime.UtcNow.Subtract(TimeSpan.FromMinutes(rollbackInMinutes));

            var kafkaTimestamp = new Timestamp(dateWithRollback, TimestampType.CreateTime);
            var topicPartitionTimestamps = new List<TopicPartitionTimestamp> { new TopicPartitionTimestamp(new TopicPartition(_topic, topicPartition), kafkaTimestamp) };
            var offsetByTimestamp = _consumer.OffsetsForTimes(topicPartitionTimestamps, TimeSpan.FromSeconds(1)).FirstOrDefault();

            if (offsetByTimestamp == null)
            {
                _logger.LogError($"OffsetsForTimes returned null for topic {_topic} with rollback {rollbackInMinutes} minutes");
                return Offset.Unset;
            }

            var offset = offsetByTimestamp.Offset.Value;
            _logger.LogDebug($"Topic {_topic} offset with rollback {rollbackInMinutes} minutes value: {offset}");

            return offset;
        }
    }
}