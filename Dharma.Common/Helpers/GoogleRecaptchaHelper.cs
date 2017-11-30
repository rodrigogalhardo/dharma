using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dharma.Common.Helpers
{
    /// <summary>
    /// Classe responsavel para validação de recaptcha
    /// </summary>
    public static class GoogleRecaptchaHelper
    {
        private static HttpClient _httpClient;

        static GoogleRecaptchaHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
        }

        /// <summary>
        /// Verifica se o recaptcha é valido usando a google API recaptcha
        /// </summary>
        /// <param name="recaptchaSecretKey"> Key do recaptcha para a aplicação</param>
        /// <param name="recaptcha">Valor informado pelo usuario ao concluir o chalenge do recaptcha na tela.</param>
        /// <param name="recaptchaUrlValidator">Url para validação do recaptcha no google</param>
        /// <returns>
        /// Verdadeiro (true) se o recapcha for válido ou Falso (false) para inválido.
        /// </returns>
        public static async Task<bool> ValidateRecaptcha(string recaptchaUrlValidator, string recaptchaSecretKey, string recaptcha)
        {
            var queryString = QueryString.Create(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("response", recaptcha),
                new KeyValuePair<string, string>("secret", recaptchaSecretKey)
            });

            var url = recaptchaUrlValidator + queryString.ToUriComponent();

            var response = await _httpClient.PostAsync(url, null);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            var responseObj = JsonConvert.DeserializeObject<RecaptchaValidationResult>(responseBody);
            return responseObj.Success;
        }
    }

    /// <summary>
    /// Objeto para capturar o retorno do status do recaptcha
    /// </summary>
    /// <summary>
    /// 
    /// </summary>
    public class RecaptchaValidationResult
    {
        /// <summary>
        /// Indica se a validação do token foi efetuada com sucesso
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("challenge_ts")]
        public DateTime Challenge { get; set; }

        /// <summary>
        /// Hostname do site onde o reCAPTCHA foi resolvido
        /// </summary>
        [JsonProperty("hostname")]
        public string HostName { get; set; }

        /// <summary>
        /// Códigos de erro
        /// </summary>
        [JsonProperty("error-codes")]
        public string[] ErrorCodes { get; set; }
    }
}
