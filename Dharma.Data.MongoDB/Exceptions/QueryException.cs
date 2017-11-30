using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para query do banco de dados que foi informada em formato inválido.
    /// </summary>
    public class QueryException : ArgumentException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        public QueryException() : base("A consulta informada deve ser informada e válida.")
        {

        }
    }
}
