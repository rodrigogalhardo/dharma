using Dharma.Common.Configurations;

namespace Dharma.Application.Configurations
{
    /// <summary>
    /// Opções da configuração do recaptcha.
    /// </summary>
    public class RecaptchaOptions : BaseOptions
    {
        /// <summary>
        /// Chave secreta do recaptcha.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Chave do site com recaptcha.
        /// </summary>
        public string SiteKey { get; set; }

        /// <summary>
        /// Url do recaptcha para validação da autenticidade do hash do recaptcha.
        /// </summary>
        public string UrlValidator { get; set; }

        /// <summary>
        /// Nome da seção no arquivo de configuração. Usado pela classe OptionsHelper.
        /// </summary>
        /// <returns>Nome da seção no arquivo de configuração.</returns>
        public override string Name()
        {
            return "Recaptcha";
        }
    }
}
