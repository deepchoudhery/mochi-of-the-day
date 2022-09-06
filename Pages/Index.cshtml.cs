using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MochiOfTheDay.Models;
using MochiOfTheDay.Services;
using MongoDB.Driver.Core.Configuration;

namespace MochiOfTheDay.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly WordsService _wordsService;
    BlobContainerClient _container;
    public IndexModel(ILogger<IndexModel> logger, WordsService wordsService)
    {
        _logger = logger;
        _wordsService = wordsService;
        _container = new BlobContainerClient
              (new Uri(@"https://mochi.blob.core.windows.net/mochi-pics"));
    }

    public async Task OnGet()
    {
        var word = _wordsService.Get(DateOnly.FromDateTime(DateTime.Now));
        if (word == null)
        {
            //word = await WordOfTheDayService.GetWordOfTheDayAsync("asdasdasd");
            if (word == null)
            {
            }
        }
        // List blobs in the container.
        // Note this is only possible when the container supports full public read access.
        foreach (BlobItem blobItem in _container.GetBlobs())
        {
            Console.WriteLine(_container.GetBlockBlobClient(blobItem.Name).Uri);
        }
    }
}
