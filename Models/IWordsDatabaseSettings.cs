namespace MochiOfTheDay.Models
{
    public interface IWordsDatabaseSettings
    {
        string? WordsCollectionName { get; set; }
        string? ConnectionString { get; set; }
        string? DatabaseName { get; set; }
    }
}
