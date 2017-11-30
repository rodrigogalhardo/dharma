using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para Id de uma entidade em formato inválido.
    /// </summary>
    public class EntityIdInvalidArgumentException : ArgumentException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="fieldName">Nome do campo/parâmetro em formato inválido.</param>
        public EntityIdInvalidArgumentException(string fieldName) : base("O campo " + fieldName + " deve ser informado e válido.", fieldName)
        {

        }
    }
}
