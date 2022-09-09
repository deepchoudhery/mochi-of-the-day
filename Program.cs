using Microsoft.Extensions.Options;
using MochiOfTheDay.Models;
using MochiOfTheDay.Services;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

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
