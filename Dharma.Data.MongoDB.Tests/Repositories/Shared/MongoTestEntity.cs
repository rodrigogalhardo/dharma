using Dharma.Data.MongoDB.Entities;

namespace Dharma.Data.MongoDB.Tests.Repositories.Shared
{
    public class MongoTestEntity : MongoEntityBase
    {
        public int Number { get; set; }

        public string String { get; set; }

        public bool Bool { get; set; }

        public MongoTestEntity[] SubItems { get; set; }
    }
}
