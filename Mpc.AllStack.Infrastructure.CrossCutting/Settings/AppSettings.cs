namespace Mpc.AllStack.Infrastructure.CrossCutting.Settings
{
    public class AppSettings
    {
        public NasaSettings Nasa { get; set; }

        public RedisSettings Redis { get; set; }
    }
}
