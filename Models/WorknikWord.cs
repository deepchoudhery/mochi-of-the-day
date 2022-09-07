using System.Text.Json.Serialization;

namespace MochiOfTheDay.Models
{
    public class WordnikWord
    {
        [JsonPropertyName("word")]
        public string? Word { get; set; }

        [JsonPropertyName("definitions")]
        public List<Definition>? Definitions { get; set; }
    }

    public class Definition
    {
        public string? Source { get; set; }
        public string? Text { get; set; }
        public object? Note { get; set; }
        public string? PartOfSpeech { get; set; }
    }
}
