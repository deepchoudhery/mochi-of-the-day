namespace MochiOfTheDay.Models
{
    public class WordsDatabaseSettings : IWordsDatabaseSettings
    {
        public string? WordsCollectionName { get; set; }
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
    }
}
