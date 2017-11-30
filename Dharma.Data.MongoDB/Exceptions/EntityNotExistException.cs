using System;

namespace Dharma.Data.MongoDB.Exceptions
{
    /// <summary>
    /// Exceção para registro que deveria existir mas não existe.
    /// </summary>
    public class EntityNotExistException : ArgumentOutOfRangeException
    {
        /// <summary>
        /// Construtor.
        /// </summary>
        /// <param name="idValue">Valor do identificador único utilizado para encontrar a entidade que não foi encontrada.</param>
        public EntityNotExistException(string idValue) : base("A entidade com identificador " + idValue + " não existe.")
        {

        }
    }
}
