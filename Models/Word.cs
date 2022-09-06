using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MochiOfTheDay.Models
{
    public class Word
    {
        [BsonElement("WordOfTheDay")]
        public string? WordOfTheDay { get; set; }

        [BsonElement("Definition")]
        public string? Definition { get; set; }

        [BsonElement("PhotoName")]
        public string? PhotoName { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Date { get; set; }
    }
}
