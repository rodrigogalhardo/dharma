namespace Dharma.Application.Configurations
{
    /// <summary>
    /// Opções da configuração de segurança para a API.
    /// </summary>
    public class SecurityApiOptions : SecurityBaseOptions
    {
        /// <summary>
        /// Nome da API.
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// Segredo da API. Usado para Reference Token.
        /// </summary>
        public string ApiSecret { get; set; }
    }
}
