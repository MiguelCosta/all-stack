namespace Mpc.AllStack.Infrastructure.CrossCutting.Settings
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; }

        public bool Enable { get; set; }

        public string KeyPrefix { get; set; }
    }
}
