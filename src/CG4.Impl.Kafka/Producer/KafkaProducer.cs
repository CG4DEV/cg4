using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;

namespace CG4.Impl.Kafka.Producer
{
    public class KafkaProducer : IKafkaProducer, IDisposable
    {
        private readonly ILogger _logger;

        private readonly string _sessionId;
        private readonly IProducer<string, string> _producer;
        private readonly JsonSerializerOptions _serializerSettings;

        private bool _disposed;

        public KafkaProducer(ILogger<KafkaProducer> logger, IKafkaSettings settings)
        {
            _logger = logger;

            _serializerSettings = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };

            _sessionId = $"{Environment.MachineName}/{DateTime.UtcNow:o}";

            _producer = new ProducerBuilder<string, string>(GetProducerConfig(settings))
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .SetLogHandler(LogHandler)
                .SetErrorHandler(ErrorHandler)
                .Build();

            _disposed = false;
        }

        public Task SendAsync<T>(T obj, string topic, object key = null)
        {
            if (obj == null)
            {
                throw new KafkaProducerException("Notification is null");
            }

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new KafkaProducerException("Topic not specified");
            }

            var objStr = JsonSerializer.Serialize(obj, _serializerSettings);

            var msg = new Message<string, string>();
            if (key == null)
            {
                key = new Key
                {
                    Type = typeof(T).Name,
                    SessionId = _sessionId,
                    Timestamp = DateTime.UtcNow.ToString(),
                };
            }

            msg.Key = JsonSerializer.Serialize(key, _serializerSettings);
            msg.Value = objStr;

            return SendAsync(msg, topic);
        }

        protected virtual async Task SendAsync(Message<string, string> message, string topic)
        {
            try
            {
                var dr = await _producer.ProduceAsync(topic, message);

                if (dr.Status == PersistenceStatus.NotPersisted)
                {
                    throw new KafkaProducerException("Message wasn't transmit");
                }
            }
            catch (KafkaException ex)
            {
                throw new KafkaProducerException("Error in kafka on send message: " + ex.Message);
            }
        }

        private static ProducerConfig GetProducerConfig(IKafkaSettings settings)
        {
            return new ProducerConfig(settings.Config ?? new Dictionary<string, string>());
        }

        private void LogHandler(IProducer<string, string> producer, LogMessage log)
        {
            _logger?.LogInformation("{level} {name}: {message}. Session: {session}", log.Level, log.Name, log.Message, _sessionId);
        }

        private void ErrorHandler(IProducer<string, string> producer, Error error)
        {
            if (error.IsFatal)
            {
                _logger?.LogCritical("[{code}] IsBroker: {isBrocker}. {reason}", error.Code, error.IsBrokerError, error.Reason);
            }
            else
            {
                _logger?.LogError("[{code}] IsBroker: {isBrocker}. {reason}", error.Code, error.IsBrokerError, error.Reason);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _producer?.Dispose();
            }

            _disposed = true;
        }
    }
}