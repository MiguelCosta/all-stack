namespace Mpc.AllStack.Infrastructure.CrossCutting.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mpc.AllStack.Infrastructure.CrossCutting.Cache;
    using Mpc.AllStack.Infrastructure.CrossCutting.Cache.Redis;
    using StackExchange.Redis.Extensions.Newtonsoft;

    public static class IoCConfig
    {
        public static IServiceCollection ConfigureDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<IRedisConnection, RedisConnection>();
            services.AddSingleton<ICacheProvider, RedisProvider>();
            services.AddSingleton<StackExchange.Redis.Extensions.Core.ISerializer, NewtonsoftSerializer>();

            return services;
        }
    }
}
