using Azure.Storage.Blobs;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MochiOfTheDay.Models;
using MochiOfTheDay.Services;
using MongoDB.Driver;
using System.Security.Authentication;
using System.Xml.Xsl;

var builder = WebApplication.CreateBuilder(args);
/*string connectionString =
  @"mongodb://mochiwords:qtb3ipBpOv2T277fcJUbEiPGpO6j557J8ym6kPmeJHdnN9X0qxdon8WILzdiA4GKAa0MFuGsoieFFf3s3Q5m6g==@mochiwords.mongo.cosmos.azure.com:10255/?ssl=true&retrywrites=false&replicaSet=globaldb&maxIdleTimeMS=120000&appName=@mochiwords@";
MongoClientSettings settings = MongoClientSettings.FromUrl(
new MongoUrl(connectionString)
);
settings.SslSettings =
  new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
var mongoClient = new MongoClient(settings);
var dbs = mongoClient.ListDatabases().ToList();
var db = dbs.First();*/

builder.Services.Configure<WordsDatabaseSettings>(builder.Configuration.GetSection(nameof(WordsDatabaseSettings)));
builder.Services.AddSingleton<IWordsDatabaseSettings>(sp => sp.GetRequiredService<IOptions<WordsDatabaseSettings>>().Value);

builder.Services.AddSingleton<WordsService>();


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
