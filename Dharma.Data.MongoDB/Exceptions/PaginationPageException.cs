using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para o campo de paginação 'page' que foi informado com valor inválido.
    /// </summary>
    public class PaginationPageException : ArgumentException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="parameterName">Nome do parâmetro.</param>
        public PaginationPageException(string parameterName) : base("O parâmetro '" + parameterName + "' deve ser um valor maior que 0 (zero).")
        {

        }
    }
}
