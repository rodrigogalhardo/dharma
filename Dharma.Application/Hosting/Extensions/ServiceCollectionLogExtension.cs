using Dharma.Application.Configurations;
using Dharma.Common.Helpers;
using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dharma.Application.Hosting.Extensions
{
    // <summary>
    /// Extensões para configurações da injeção de dependência para os logs.
    /// </summary>
    public static class ServiceCollectionLogExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <param name="configuration">Configuração de inicilização.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddLog(this IServiceCollection services, IConfiguration configuration)
        {
            var config = OptionsHelper.GetConfiguration<StackdriverOptions>(configuration);

            services.AddGoogleExceptionLogging(options =>
            {
                options.ProjectId = config.ProjectId;
                options.ServiceName = ApplicationHelper.Name();
                options.Version = ApplicationHelper.Version();
            });

            services.AddGoogleTrace(options =>
            {
                options.ProjectId = config.ProjectId;
            });

            return services;
        }
    }
}
