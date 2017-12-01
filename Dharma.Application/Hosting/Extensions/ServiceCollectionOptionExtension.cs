using Dharma.Application.Configurations;
using Dharma.Application.Hosting.Enums;
using Dharma.Common.Configurations;
using Dharma.Common.Helpers;
using Dharma.Data.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dharma.Application.Hosting.Extensions
{
    // <summary>
    /// Extensões para configurações da injeção de dependência para as configurações.
    /// </summary>
    public static class ServiceCollectionOptionExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <param name="startup">Inicializador da hospedagem.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddConfigurations(this IServiceCollection services, BaseStartup startup)
        {
            services.ConfigureOption<MongoOptions>(startup.Configuration);

            switch (startup.Startup)
            {
                case StartupEnum.UI:
                    services.ConfigureOption<SecurityUIOptions>(startup.Configuration);
                    services.ConfigureOption<RecaptchaOptions>(startup.Configuration);
                    break;

                case StartupEnum.API:
                    services.ConfigureOption<SecurityApiOptions>(startup.Configuration);
                    break;

                default:
                    throw new NotImplementedException("Não implementado camada de configuração para o tipo: " + Enum.GetName(typeof(StartupEnum), startup.Startup) + ".");
            }

            return services;
        }

        public static IServiceCollection ConfigureOption<T>(this IServiceCollection services, IConfiguration configuration) where T : BaseOptions
        {
            services.Configure<T>(OptionsHelper.GetSection<T>(configuration));

            return services;
        }
    }
}
