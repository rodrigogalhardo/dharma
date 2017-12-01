using Dharma.Common.Configurations;

namespace Dharma.Application.Configurations
{
    /// <summary>
    /// Classe base para classes de configurações de segurança.
    /// </summary>
    public class SecurityBaseOptions : BaseOptions
    {
        /// <summary>
        /// URL do autorizador.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Nome da seção no arquivo de configuração. Usado pela classe OptionsHelper.
        /// </summary>
        /// <returns>Nome da seção no arquivo de configuração.</returns>
        public override string Name()
        {
            return "Security";
        }
    }
}
