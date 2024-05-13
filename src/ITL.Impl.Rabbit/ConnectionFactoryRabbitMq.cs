using RabbitMQ.Client;

namespace ITL.Impl.Rabbit
{
    /// <summary>
    /// Фабрика подключений к RabbitMQ
    /// </summary>
    public class ConnectionFactoryRabbitMQ : IConnectionFactory, IDisposable
    {
        private readonly object _lock = new object();
        private bool disposed;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;

        // Maybe pass IConnection Factory in constructor? - Can't test this
        public ConnectionFactoryRabbitMQ(IConnectionFactorySettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _connectionFactory = new ConnectionFactory()
            {
                HostName = settings.Host,
                Port = settings.Port,
                VirtualHost = settings.VirtualHost,
                UserName = settings.Login,
                Password = settings.Password,
                DispatchConsumersAsync = settings.DispatchConsumersAsync,
                ClientProvidedName = settings.ClientProvidedName,
            };
        }

        ~ConnectionFactoryRabbitMQ()
        {
            Dispose(false);
        }

        public IDictionary<string, object> ClientProperties
        {
            get => _connectionFactory.ClientProperties;
            set => _connectionFactory.ClientProperties = value;
        }

        public string Password
        {
            get => _connectionFactory.Password;
            set => _connectionFactory.Password = value;
        }

        public ushort RequestedChannelMax
        {
            get => _connectionFactory.RequestedChannelMax;
            set => _connectionFactory.RequestedChannelMax = value;
        }

        public uint RequestedFrameMax
        {
            get => _connectionFactory.RequestedFrameMax;
            set => _connectionFactory.RequestedFrameMax = value;
        }

        public TimeSpan RequestedHeartbeat
        {
            get => _connectionFactory.RequestedHeartbeat;
            set => _connectionFactory.RequestedHeartbeat = value;
        }

        public bool UseBackgroundThreadsForIO
        {
            get => _connectionFactory.UseBackgroundThreadsForIO;
            set => _connectionFactory.UseBackgroundThreadsForIO = value;
        }

        public string UserName
        {
            get => _connectionFactory.UserName;
            set => _connectionFactory.UserName = value;
        }

        public string VirtualHost
        {
            get => _connectionFactory.VirtualHost;
            set => _connectionFactory.VirtualHost = value;
        }

        public string HostName
        {
            get => _connectionFactory.HostName;
            set => _connectionFactory.HostName = value;
        }

        public int Port
        {
            get => _connectionFactory.Port;
            set => _connectionFactory.Port = value;
        }

        public Uri Uri
        {
            get => _connectionFactory.Uri;
            set => _connectionFactory.Uri = value;
        }

        public TimeSpan HandshakeContinuationTimeout
        {
            get => _connectionFactory.HandshakeContinuationTimeout;
            set => _connectionFactory.HandshakeContinuationTimeout = value;
        }

        public TimeSpan ContinuationTimeout
        {
            get => _connectionFactory.ContinuationTimeout;
            set => _connectionFactory.ContinuationTimeout = value;
        }

        public TimeSpan NetworkRecoveryInterval
        {
            get => _connectionFactory.NetworkRecoveryInterval;
            set => _connectionFactory.NetworkRecoveryInterval = value;
        }

        public bool AutomaticRecoveryEnabled
        {
            get => _connectionFactory.AutomaticRecoveryEnabled;
            set => _connectionFactory.AutomaticRecoveryEnabled = value;
        }

        public string ClientProvidedName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICredentialsProvider CredentialsProvider { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ICredentialsRefresher CredentialsRefresher { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IAuthMechanismFactory AuthMechanismFactory(IList<string> mechanismNames)
        {
            return _connectionFactory.AuthMechanismFactory(mechanismNames);
        }

        public IConnection CreateConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                lock (_lock)
                {
                    if (_connection == null || !_connection.IsOpen)
                    {
                        _connection = _connectionFactory.CreateConnection();
                    }
                }
            }

            return _connection;
        }

        private IConnection CreateConnectionInternal(Func<IConnection> func)
        {
            if (_connection == null || !_connection.IsOpen)
            {
                lock (_lock)
                {
                    if (_connection == null || !_connection.IsOpen)
                    {
                        _connection = func();
                    }
                }
            }

            return _connection;
        }

        public IConnection CreateConnection(string clientProvidedName)
        {
            return CreateConnectionInternal(() => _connectionFactory.CreateConnection(clientProvidedName));
        }

        public IConnection CreateConnection(IList<string> hostnames)
        {
            return CreateConnectionInternal(() => _connectionFactory.CreateConnection(hostnames));
        }

        public IConnection CreateConnection(IList<string> hostnames, string clientProvidedName)
        {
            return CreateConnectionInternal(() => _connectionFactory.CreateConnection(hostnames, clientProvidedName));
        }

        public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints)
        {
            return CreateConnectionInternal(() => _connectionFactory.CreateConnection(endpoints));
        }

        public IConnection CreateConnection(IList<AmqpTcpEndpoint> endpoints, string clientProvidedName)
        {
            return CreateConnectionInternal(() => _connectionFactory.CreateConnection(endpoints, clientProvidedName));
        }

        public void Dispose()
        {
            Dispose(true);

            // подавляем финализацию
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && _connection != null)
                {
                    // Освобождаем управляемые ресурсы
                    _connection.Close();
                    _connection.Dispose();
                }

                // освобождаем неуправляемые объекты
                _connection = null;
                _connectionFactory = null;
                disposed = true;
            }
        }
    }
}
