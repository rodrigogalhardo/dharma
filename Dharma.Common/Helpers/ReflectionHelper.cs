using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dharma.Common.Helpers
{
    /// <summary>
    /// Operações para auxiliar na manipulação de objetos por reflexão.
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo genérico.
        /// </summary>
        /// <param name="type">Tipo do genérico que será buscado as classes que o herdam.</param>
        /// <returns>Lista de tipos que herdam o genérico.</returns>
        public static IEnumerable<Type> ListClassesInheritFromGeneric(Type type)
        {
            return GetLocalAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t =>
                    t.BaseType != null
                    && t.BaseType.IsGenericType
                    && t.BaseType.GetGenericTypeDefinition() == type);
        }

        /// <summary>
        /// Lista todas as classes do Assembly que herdam de um tipo.
        /// </summary>
        /// <param name="type">Tipo do genérico que será buscado as classes que o herdam.</param>
        /// <returns>Lista de tipos que herdam o genérico.</returns>
        public static IEnumerable<Type> ListClassesInheritFromType(Type type)
        {
            return GetLocalAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(t =>
                    t.BaseType != null && t.BaseType == type);
        }

        private static IEnumerable<Assembly> GetLocalAssemblies()
        {
            var assemblyInfo = Assembly.GetExecutingAssembly().FullName.Split(',');
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var namespaces = assemblyInfo[0].Split('.');
            var assembliesToSearch = assemblies.Where(x => x.FullName.StartsWith(namespaces[0]));

            return assembliesToSearch;
        }
    }
}
