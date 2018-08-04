namespace Mpc.AllStack.Application.Services.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class IoCConfig
    {
        public static IServiceCollection ConfigureDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<INasaService, NasaService>();

            return services;
        }
    }
}
