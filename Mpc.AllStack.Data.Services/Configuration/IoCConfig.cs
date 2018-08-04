namespace Mpc.AllStack.Data.Services.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class IoCConfig
    {
        public static IServiceCollection ConfigureDependencies(
            this IServiceCollection services, 
            IConfiguration configuration)
        {
            // AppSettings
            services.AddSingleton<Nasa.INasaClientFactory, Nasa.NasaClientFactory>();
            services.AddSingleton<Nasa.INasaClient, Nasa.NasaClient>();

            return services;
        }
    }
}
