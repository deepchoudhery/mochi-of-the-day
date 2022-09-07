using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MochiOfTheDay.Models;
using MongoDB.Driver;
using System.Text.Json;

namespace MochiOfTheDay.Services
{
    public class WordsService
    {
        private readonly IMongoCollection<Word> _words;
        public WordsService(IWordsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _words = database.GetCollection<Word>(settings.WordsCollectionName);
        }

        public List<Word> Get() => _words.Find(Word => true).ToList();

        public Word Get(string id) => _words.Find(Word => id.Equals(Word.Date)).FirstOrDefault();

        public Word Create(Word Word)
        {
            _words.InsertOne(Word);
            return Word;
        }

        public void Update(string id, Word updatedWord) => _words.ReplaceOne(Word => id.Equals(Word.Date), updatedWord);

        public void Delete(Word WordForDeletion) => _words.DeleteOne(Word => Word.Date.Equals(WordForDeletion.Date));

        public void Delete(string id) => _words.DeleteOne(Word => id.Equals(Word.Date));
    }

    public class WordOfTheDayService
    {
        public static async Task<Word> GetWordOfTheDayAsync(string photoName, string apiKey)
        {
            string address = $"http://api.wordnik.com/v4/words.json/wordOfTheDay?api_key={apiKey}";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            string result = await response.Content.ReadAsStringAsync();
            WordnikWord? randomApiWord = null;
            if (!string.IsNullOrEmpty(result))
            {
                randomApiWord = JsonSerializer.Deserialize<WordnikWord>(result, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            
            if (randomApiWord == null)
            {
                throw new Exception("asdfasdf");
            }

            Word newWord = new()
            {
                WordOfTheDay = randomApiWord.Word,
                Definition = randomApiWord.Definitions?.First().Text,
                Date = new Tuple<int, int, int>(DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year).ToString(),
                PhotoUrl = photoName,
                PartOfSpeech = randomApiWord.Definitions?.First().PartOfSpeech
            };

            return newWord;
        }

        public static async Task<string> GetRandomPic(BlobContainerClient blobContainer)
        {
            // List blobs in the container.
            // Note this is only possible when the container supports full public read access.
            var allPics = new List<BlobItem>();
            await foreach (var blobItem in blobContainer.GetBlobsAsync())
            {
                allPics.Add(blobItem);
            }
            BlobItem? randomPic = null;
            if (allPics != null && allPics.Any())
            {
                randomPic = allPics.ToArray()[new Random().Next(0, allPics.Count())];
            }

            if (randomPic == null)
            {
                return "404";
            }
           return randomPic?.Name ?? string.Empty;
        }
    }
}
