using Dharma.Common.Helpers;
using Dharma.Data.MongoDB;
using Dharma.Data.MongoDB.Providers;
using Dharma.Data.MongoDB.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Dharma.Application.Hosting.Extensions
{
    /// <summary>
    /// Extensões para as configurações de repositórios.
    /// </summary>
    public static class ServiceCollectionRepositoryExtension
    {
        /// <summary>
        /// Configura os valores padrões para as chaves de configuração do sistema.
        /// </summary>
        /// <param name="services">Configurador de serviços da aplicação web.</param>
        /// <param name="startup">Inicializador da hospedagem.</param>
        /// <returns>Configurador de serviços da aplicação web.</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services, BaseStartup startup)
        {
            var all = ReflectionHelper.ListClassesInheritFromGeneric(typeof(MongoRepository<>));

            var mongoOptions = Options.Create(OptionsHelper.GetConfiguration<MongoOptions>(startup.Configuration));

            var provider = new MongoDatabaseProvider(mongoOptions);

            foreach (var item in all)
            {
                services.AddSingleton(item);

                var nameMethod = item.GetMethod("CreateIndexes");

                nameMethod.Invoke(Activator.CreateInstance(item, provider, null), null);
            }

            return services;
        }
    }
}
