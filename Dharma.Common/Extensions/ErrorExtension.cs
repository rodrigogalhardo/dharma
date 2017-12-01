using Dharma.Common.Extensions.Shared;
using System;

namespace Dharma.Common.Extensions
{
    /// <summary>
    /// Operações auxiliares para extração da mensagem de erro de um enumerador.
    /// </summary>
    public static class ErrorExtension
    {
        /// <summary>
        /// Obtém a representação do erro que está em um enumerador.
        /// </summary>
        /// <param name="error">Erro ocorrido.</param>
        /// <param name="parameters">Parâmetros do erro.</param>
        /// <returns>Objeto com os dados do erro.</returns>
        public static Error ErrorDescription(this Enum error, params string[] parameters)
        {
            string description = error.Description();

            string message = string.Empty;

            if (parameters == null)
            {
                message = description;
            }
            else
            {
                message = string.Format(description, parameters);
            }

            int code = (int)Convert.ChangeType(error, error.GetTypeCode());

            return new Error()
            {
                Code = code.ToString("000"),
                Message = message
            };
        }
    }
}
