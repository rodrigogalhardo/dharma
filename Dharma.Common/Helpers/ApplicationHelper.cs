using Microsoft.Extensions.PlatformAbstractions;

namespace Dharma.Common.Helpers
{
    /// <summary>
    /// Operações para auxiliar obtenção de dados da aplicação.
    /// </summary>
    public static class ApplicationHelper
    {
        /// <summary>
        /// Versão da aplicação atual.
        /// </summary>
        /// <returns>Número da versão atual.</returns>
        public static string Version()
        {
            return PlatformServices.Default.Application.ApplicationVersion;
        }

        /// <summary>
        /// Nome da aplicação em execução.
        /// </summary>
        /// <returns>Nome da aplicação em execução.</returns>
        public static string Name()
        {
            return PlatformServices.Default.Application.ApplicationName;
        }
    }
}
