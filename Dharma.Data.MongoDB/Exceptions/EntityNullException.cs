using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para entidade que deve ser informada, mas foi informada como null.
    /// </summary>
    public class EntityNullException : ArgumentException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="parameterName">Nome do parâmetro que deveria ser informado, mas foi informado como nulo.</param>
        public EntityNullException(string parameterName) : base("O parâmetro '" + parameterName + "' não poder ser nulo ou se for uma lista não pode conter nenhum item nulo.")
        {

        }
    }
}
