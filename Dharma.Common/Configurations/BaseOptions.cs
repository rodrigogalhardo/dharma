namespace Dharma.Common.Configurations
{
    /// <summary>
    /// Classe base para classes de configurações.
    /// </summary>
    public abstract class BaseOptions
    {
        /// <summary>
        /// Nome da seção no arquivo de configuração. Usado pela class OptionsHelper.
        /// </summary>
        /// <returns>Nome da seção no arquivo de configuração.</returns>
        public abstract string Name();
    }
}
