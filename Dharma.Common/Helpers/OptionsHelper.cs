using Dharma.Common.Configurations;
using Dharma.Common.Helpers.Exceptions;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Dharma.Common.Helpers
{
    /// <summary>
    /// Operações para auxiliar na manipulação de configurações.
    /// </summary>
    public static class OptionsHelper
    {
        /// <summary>
        /// Obtém a seção de uma configuração.
        /// </summary>
        /// <typeparam name="T">Tipo da option.</typeparam>
        /// <param name="configuration">Configuração carregada.</param>
        /// <returns>Seção da configuração.</returns>
        public static IConfigurationSection GetSection<T>(IConfiguration configuration) where T : BaseOptions
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            var instance = Activator.CreateInstance<T>();

            var exists = configuration.GetChildren().Any(item => item.Key == instance.Name());

            if (!exists)
            {
                throw new SectionNotExistException(instance.Name());
            }

            var nameMethod = typeof(T).GetMethod("Name");

            var name = nameMethod.Invoke(instance, null);

            return configuration.GetSection(name.ToString());
        }

        /// <summary>
        /// Obtém a seção de uma configuração.
        /// </summary>
        /// <typeparam name="T">Tipo da option.</typeparam>
        /// <param name="configuration">Configuração carregada.</param>
        /// <returns>Seção da configuração.</returns>
        public static T GetConfiguration<T>(IConfiguration configuration) where T : BaseOptions
        {
            var section = GetSection<T>(configuration);

            var instance = Activator.CreateInstance<T>();

            section.Bind(instance);

            return instance;
        }
    }
}
