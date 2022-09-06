using Azure.Storage.Blobs;
using MochiOfTheDay.Models;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public Word Get(DateOnly id) => _words.Find(Word => Word.Date.Equals(id)).FirstOrDefault();

        public Word Create(Word Word)
        {
            _words.InsertOne(Word);
            return Word;
        }

        public void Update(DateOnly id, Word updatedWord) => _words.ReplaceOne(Word => Word.Date.Equals(id), updatedWord);

        public void Delete(Word WordForDeletion) => _words.DeleteOne(Word => Word.Date.Equals(WordForDeletion.Date));

        public void Delete(DateOnly id) => _words.DeleteOne(Word => Word.Date.Equals(id));
    }

    public class WordOfTheDayService
    {
        public static async Task<Word> GetWordOfTheDayAsync(string photoName)
        {
            string address = "http://api.wordnik.com/v4/words.json/wordOfTheDay?api_key=";
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(address);
            response.EnsureSuccessStatusCode();
            //result = await response.Content.ReadAsStringAsync();

            //call api to get word of the day
            Word newWord = new()
            {
                WordOfTheDay = "hurrdurr",
                Definition = "hurrdurr",
                Date = DateTime.Now,
                PhotoName = photoName
            };
            return newWord;
        }
    }
}
