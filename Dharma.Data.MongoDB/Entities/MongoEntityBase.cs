using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Dharma.Data.MongoDB.Entities
{
    /// <summary>
    /// Entidade base para todas as outras entidades.
    /// </summary>
    public class MongoEntityBase
    {
        /// <summary>
        /// Identificador único do registro no banco de dados.
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        /// <summary>
        /// Data de criação do registro.
        /// </summary>
        public DateTime Created { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Data da última atualização do registro.
        /// </summary>
        public DateTime Updated { get; set; } = DateTime.UtcNow;
    }
}
