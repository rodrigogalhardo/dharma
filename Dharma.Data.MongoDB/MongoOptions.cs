using Dharma.Common.Configurations;

namespace Dharma.Data.MongoDB
{
    /// <summary>
    /// Opções de comunicação com o banco de dados (Cassandra).
    /// </summary>
    public class MongoOptions : BaseOptions
    {
        /// <summary>
        /// URL do banco de dados.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Nome do banco de dados.
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Nome da seção no arquivo de configuração. Usado pela classe OptionsHelper.
        /// </summary>
        /// <returns>Nome da seção no arquivo de configuração.</returns>
        public override string Name()
        {
            return "Mongo";
        }
    }
}
