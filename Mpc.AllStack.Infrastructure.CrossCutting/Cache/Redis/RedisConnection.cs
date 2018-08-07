namespace Mpc.AllStack.Infrastructure.CrossCutting.Cache.Redis
{
    using System;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core;

    public class RedisConnection : IRedisConnection
    {
        private readonly Settings.RedisSettings configuration;
        private readonly Lazy<ConnectionMultiplexer> LazyConnection;

        public RedisConnection(Settings.RedisSettings configuration, ISerializer serializer)
        {
            this.configuration = configuration;

            if (this.configuration.Enable)
            {
                this.LazyConnection = InitializeConnection();
                this.Database = Connection.GetDatabase();
                this.Serializer = serializer;
                var assemblyVersion = typeof(RedisConnection).Assembly.GetName().Version.ToString();
                this.KeyPrefix = configuration.KeyPrefix + assemblyVersion;
            }
        }

        public IDatabase Database { get; set; }

        public ISerializer Serializer { get; }

        public string KeyPrefix { get; }

        internal ConnectionMultiplexer Connection
        {
            get
            {
                return LazyConnection.Value;
            }
        }

        public void Close()
        {
            if (Connection != null && Connection.IsConnected)
            {
                Connection.Close();
            }
        }

        private ConfigurationOptions GetConnectionStringConfiguration()
        {
            try
            {
                var options = ConfigurationOptions.Parse(configuration.ConnectionString);
                return options;
            }
            catch (Exception ex)
            {
                //Log.Current.Warning("Unable to parse Redis Connection String", () => ex);

                return GetDefaultConfiguration();
            }
        }

        private ConfigurationOptions GetDefaultConfiguration()
        {
            var options = new ConfigurationOptions
            {
                AbortOnConnectFail = false
            };

            //options.EndPoints.Add(configuration.Value.RedisMasterConnection, configuration.Value.RedisMasterPort);
            //options.EndPoints.Add(configuration.Value.RedisSlaveConnection, configuration.Value.RedisSlaverPort);
            //options.ConnectRetry = configuration.Value.RedisConnectRetry;
            //options.DefaultDatabase = configuration.Value.RedisDefaultDatabase;
            //options.KeepAlive = configuration.Value.RedisKeepAlive;
            try
            {
                options.ClientName = "PROD:" + Environment.MachineName;
            }
            catch (Exception)
            {
                options.ClientName = "PROD:" + Guid.NewGuid();
            }

            //if (!string.IsNullOrWhiteSpace(configuration.Value.RedisMasterAccessKey))
            //{
            //    options.Password = configuration.Value.RedisMasterAccessKey;
            //}

            return options;
        }

        private Lazy<ConnectionMultiplexer> InitializeConnection()
        {
            return new Lazy<ConnectionMultiplexer>(() =>
            {
                var connection = ConnectionMultiplexer.Connect(GetConnectionStringConfiguration());

                connection.IncludeDetailInExceptions = true;
                //connection.ConnectionFailed += (sender, args) => Log.Current.Fatal("REDIS ConnectionFailed", () => args);
                //connection.ConfigurationChangedBroadcast += (sender, args) => Log.Current.Fatal("REDIS ConfigurationChangedBroadcast", () => args.EndPoint);
                //connection.ConnectionRestored += (sender, args) => Log.Current.Fatal("REDIS ConnectionRestored", () => args);
                //connection.InternalError += (sender, args) => Log.Current.Fatal("REDIS InternalError", () => args);
                //connection.HashSlotMoved += (sender, args) => Log.Current.Fatal("REDIS HashSlotMoved", () => args);
                //connection.ErrorMessage += (sender, args) => Log.Current.Fatal("REDIS ErrorMessage", () => args);
                //connection.ConfigurationChanged += (sender, args) => Log.Current.Fatal("REDIS ConfigurationChanged", () => args.EndPoint);

                return connection;
            });
        }
    }
}
