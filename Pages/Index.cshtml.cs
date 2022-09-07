using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MochiOfTheDay.Models;
using MochiOfTheDay.Services;

namespace MochiOfTheDay.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IConfiguration _configuration;
    private readonly WordsService _wordsService;
    private readonly BlobContainerClient _container;
    public string? ImgUrl { get; set; }
    public string? WordName { get; set; }
    public string? WordDefinition { get; set; }
    public string? PartOfTheSpeech { get; set; }

    public IndexModel(ILogger<IndexModel> logger, WordsService wordsService, IConfiguration configuration)
    {
        _logger = logger;
        _wordsService = wordsService;
        _configuration = configuration;
        _container = new BlobContainerClient
              (new Uri(@"https://mochi.blob.core.windows.net/mochi-pics"));
    }

    public async Task OnGet()
    {
        string dateTimeNow = new Tuple<int, int, int>(DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Year).ToString();
        Word? word = _wordsService.Get(dateTimeNow);
        if (word == null)
        {
            var randomPicUrl = await WordOfTheDayService.GetRandomPic(_container);
            var wornikApiKey = _configuration.GetSection("WordOfTheDay:ApiKey").Value;
            if (string.IsNullOrEmpty(wornikApiKey))
            {
                //throw Exception
            }
            else
            {
                word = await WordOfTheDayService.GetWordOfTheDayAsync(randomPicUrl, wornikApiKey);
                _wordsService.Create(word);
            }
        }

        ImgUrl = _configuration.GetSection("mochipics:blob").Value + word?.PhotoUrl;
        WordName = word?.WordOfTheDay;
        WordDefinition = word?.Definition;
        PartOfTheSpeech = word?.PartOfSpeech;
    }
}
