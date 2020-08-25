using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppLocator.Infrastructure.Entities
{
    public class ApplicationDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public int Application { get; set; }

        [BsonRequired]
        public string Url { get; set; }

        [BsonRequired]
        public string PathLocal { get; set; }

        [BsonRequired]
        public bool DebuggingMode { get; set; }
    }
}
