namespace Mpc.AllStack.WebAppMvc.Configuration
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mpc.AllStack.Infrastructure.CrossCutting.Settings;

    public static class IoCConfig
    {
        public static IServiceCollection ConfigureDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // AppSettings
            var appSettings = configuration.Get<AppSettings>();
            services.AddSingleton(appSettings);
            services.AddSingleton(appSettings.Nasa);

            // Services
            Application.Services.Configuration.IoCConfig.ConfigureDependencies(services, configuration);
            Data.Services.Configuration.IoCConfig.ConfigureDependencies(services, configuration);

            return services;
        }
    }
}
