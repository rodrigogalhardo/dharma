using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para Id de uma entidade que não pode ser informado.
    /// </summary>
    public class EntityIdNullArgumentException : ArgumentException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="fieldName">Nome do campo/parâmetro que não pode ser informado.</param>
        public EntityIdNullArgumentException(string fieldName) : base("O campo " + fieldName + " não pode ser informado.", fieldName)
        {

        }
    }
}
