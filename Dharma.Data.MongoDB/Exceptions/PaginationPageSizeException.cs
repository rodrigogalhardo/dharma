using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para o campo de paginação 'pageSize' que foi informado com valor inválido.
    /// </summary>
    public class PaginationPageSizeException : ArgumentException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="parameterName">Nome do parâmetro.</param>
        public PaginationPageSizeException(string parameterName) : base("O parâmetro '" + parameterName + "' deve ser um valor maior que 0 (zero) e menor que 1000 (mil).")
        {

        }
    }
}
