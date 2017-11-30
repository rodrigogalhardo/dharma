using System;

namespace Dharma.Common.Helpers.Exceptions
{
    /// <summary>
    /// Exceção para seção que não existe no arquivo de configuração.
    /// </summary>
    public class SectionNotExistException : Exception
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="sectionName">Nome da seção de configuração com erro.</param>
        public SectionNotExistException(string sectionName) : base("A seção " + sectionName + " não existe no arquivo de configuração (appsettings.json).")
        {

        }
    }
}
