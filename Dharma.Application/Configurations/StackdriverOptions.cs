using Dharma.Common.Configurations;

namespace Dharma.Application.Configurations
{
    /// <summary>
    /// Opções de comunicação com o Stackdriver (Google).
    /// </summary>
    public class StackdriverOptions : BaseOptions
    {
        /// <summary>
        /// Identificador do projeto.
        /// </summary>
        public string ProjectId { get; set; }

        /// <summary>
        /// Nome da seção no arquivo de configuração. Usado pela classe OptionsHelper.
        /// </summary>
        /// <returns>Nome da seção no arquivo de configuração.</returns>
        public override string Name()
        {
            return "Stackdriver";
        }
    }
}
