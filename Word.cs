namespace MochiOfTheDay
{
    public class Word
    {
        public int Id { get; set; }
        public string? WordOfTheDay { get; set; }
        public string? Definition { get; set; }
        public DateOnly Date { get; set; }
    }
}
