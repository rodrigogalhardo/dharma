using Dharma.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Dharma.Application.Services
{
    /// <summary>
    /// Base para limitar as resposta dos serviços aos padrões definidos.
    /// </summary>
    /// <typeparam name="EnumError">Enumerador de erros possíveis para esse serviço.</typeparam>
    public abstract class BaseService
    {
        /// <summary>
        /// HTTP 200 (OK).
        /// </summary>
        /// <returns>HTTP 200 (OK).</returns>
        protected OkResult Ok()
        {
            return new OkResult();
        }

        /// <summary>
        /// HTTP 200 (OK) com corpo da mensagem.
        /// </summary>
        /// <param name="value">Corpo da mensagem.</param>
        /// <returns>HTTP 200 (OK) com corpo da mensagem..</returns>
        protected OkObjectResult Ok(object value)
        {
            return new OkObjectResult(value);
        }

        /// <summary>
        /// HTTP 400 (Bad Request).
        /// </summary>
        /// <param name="errorItem">Erro ocorrido.</param>
        /// <param name="parameters">Parâmetros (caso haja).</param>
        /// <returns>HTTP 400 (Bad Request).</returns>
        protected BadRequestObjectResult BadRequest(Enum errorItem, params string[] parameters)
        {
            return new BadRequestObjectResult(errorItem.ErrorDescription(parameters));
        }

        /// <summary>
        /// HTTP 404 (Not Found).
        /// </summary>
        /// <param name="errorItem">Erro ocorrido.</param>
        /// <param name="parameters">Parâmetros (caso haja).</param>
        /// <returns>HTTP 404 (Not Found).</returns>
        protected NotFoundObjectResult NotFound(Enum errorItem, params string[] parameters)
        {
            return new NotFoundObjectResult(errorItem.ErrorDescription(parameters));
        }

        /// <summary>
        /// HTTP 409 (Conflict).
        /// </summary>
        /// <param name="errorItem">Erro ocorrido.</param>
        /// <param name="parameters">Parâmetros (caso haja).</param>
        /// <returns>HTTP 409 (Conflict).</returns>
        protected ObjectResult Conflict(Enum errorItem, params string[] parameters)
        {
            var result = new ObjectResult(errorItem.ErrorDescription(parameters))
            {
                StatusCode = (int)HttpStatusCode.Conflict
            };

            return result;
        }

        /// <summary>
        /// Verificar se um valor é nulo ou ultrapassa os limites do mínimo/máximo número de caracteres.
        /// Caso viole alguma dessas regras, será retornado false e um objeto de BadRequest já configurado com
        /// o erro adequado. Caso nenhum problema seja encontrado, será retornado true e um objeto nulo.
        /// </summary>
        /// <param name="value">Valor a ser verificado.</param>
        /// <param name="minLength">Número mínimo de caracteres.</param>
        /// <param name="maxLegth">Número máximo de caracteres.</param>
        /// <param name="errorForNull">Erro que deve ser configurado caso esteja o valor seja nulo.</param>
        /// <param name="errorForLength">Erro que deve ser configurado caso o valor tenha ultrapassado os limites de caracteres mínimo ou máximo.</param>
        /// <param name="result">Objeto com o resultado de retorno.</param>
        /// <returns>Retorna true se o valor for válido, senão, false.</returns>
        protected bool IsValid(string value, int minLength, int maxLegth, Enum errorForNull, Enum errorForLength, out BadRequestObjectResult result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = BadRequest(errorForNull);
            }
            else if (value.Length < minLength || value.Length > maxLegth)
            {
                result = BadRequest(errorForLength);
            }
            else
            {
                result = null;
            }

            return result == null;
        }
    }
}
