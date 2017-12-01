namespace Dharma.Application.Configurations
{
    /// <summary>
    /// Opções da configuração de segurança para UI.
    /// </summary>
    public class SecurityUIOptions : SecurityBaseOptions
    {
        /// <summary>
        /// Identificador da aplicação cliente.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Segredo da aplicação cliente.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Escopos.
        /// </summary>
        public string[] Scopes { get; set; }
    }
}
