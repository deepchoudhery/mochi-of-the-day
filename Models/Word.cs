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

        [BsonElement("PartOfSpeech")]
        public string? PartOfSpeech { get; set; }

        [BsonElement("PhotoUrl")]
        public string? PhotoUrl { get; set; }

        [BsonId]
        public string? Date { get; set; }
    }
}
