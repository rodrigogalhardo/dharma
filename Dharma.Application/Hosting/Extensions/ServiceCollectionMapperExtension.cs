using AutoMapper;
using Dharma.Common.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Dharma.Application.Hosting.Extensions
{
    // <summary>
    /// Extensões para configurações da injeção de dependência para os mapeamentos.
    /// </summary>
    public static class ServiceCollectionMapperExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <param name="startup">Inicializador da hospedagem.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddMappers(this IServiceCollection services, BaseStartup startup)
        {
            var items = ReflectionHelper.ListClassesInheritFromType(typeof(Profile));

            services.AddAutoMapper(cfg =>
            {
                foreach (var item in items)
                {
                    cfg.AddProfile(item);
                }
            });

            return services;
        }
    }
}
