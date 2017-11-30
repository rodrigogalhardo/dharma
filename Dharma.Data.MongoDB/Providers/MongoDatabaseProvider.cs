using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Dharma.Data.MongoDB.Providers
{
    /// <summary>
    /// Fornecedor da conexão com o banco de dados (MongoDB).
    /// </summary>
    public class MongoDatabaseProvider
    {
        private readonly IOptions<MongoOptions> _configuration;

        /// <summary>
        /// Construtor do fornecedor da conexão com o banco de dados.
        /// </summary>
        /// <param name="configuration">Opções de configuração para comunicação com o banco de dados (MongoDB).</param>
        public MongoDatabaseProvider(IOptions<MongoOptions> configuration)
        {
            _configuration = configuration;

            var pack = new ConventionPack { new EnumRepresentationConvention(BsonType.String) };

            ConventionRegistry.Register("EnumStringConvention", pack, t => true);
        }

        /// <summary>
        /// Cria um objeto de conexão com o banco de dados.
        /// </summary>
        /// <returns>Objeto de conexão com o banco de dados.</returns>
        public IMongoDatabase Create()
        {
            var client = new MongoClient(Connection.ToMongoUrl());
            return client.GetDatabase(_configuration.Value.DatabaseName);
        }

        /// <summary>
        /// Objeto de configuração de cliente do banco de dados.
        /// </summary>
        protected MongoUrlBuilder Connection
        {
            get { return new MongoUrlBuilder(_configuration.Value.Url); }
        }
    }
}
