using Dharma.Application.Services;
using Dharma.Application.Services.Attributes;
using Dharma.Common.Helpers;
using Dharma.Data.MongoDB.Providers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Dharma.Application.Hosting.Extensions
{
    /// <summary>
    /// Extensões para as configurações de serviços com regras de negócio.
    /// </summary>
    public static class ServiceCollectionBaseServiceExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddBaseServices(this IServiceCollection services)
        {
            var all = ReflectionHelper.ListClassesInheritFromType(typeof(BaseService));

            foreach (var item in all)
            {
                TypeInfo typeInfo = item.GetTypeInfo();

                var attrs = typeInfo.GetCustomAttributes();

                var injectType = InjectionType.Singleton;

                foreach (var attr in attrs)
                {
                    if (attr.GetType() == typeof(InjectionTypeAttribute))
                    {
                        var attribute = item.GetTypeInfo().GetCustomAttribute<InjectionTypeAttribute>();

                        injectType = attribute.InjectionType;
                    }
                }

                if (injectType == InjectionType.Singleton)
                {
                    services.AddSingleton(item);
                }
                else if (injectType == InjectionType.Scoped)
                {
                    services.AddScoped(item);
                }
            }

            services.AddSingleton<MongoDatabaseProvider>();

            return services;
        }
    }
}
