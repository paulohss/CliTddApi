using CliTddApi.Service.Process;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

// Run the command in the terminal:
//   -> dotnet run -- wa --city London  (work async 'wa')
//   -> dotnet run -- ws --city London  (sync sync 'ws')
// Find process [CliTddApi.Cli] and attach it
Console.WriteLine("Press Enter");
Console.ReadLine(); //For debug.
//Add breakpoint here:
var builder = CoconaApp.CreateBuilder();
// Press enter in terminal to move the debug
//Program Execution:
builder.Logging.AddFilter("System.Net.Http", LogLevel.Warning);
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IWeatherService, WeatherVisualCrossingService>();
var app = builder.Build();
try
{
    app.AddCommand("wa", async (string city, IWeatherService weatherService) =>
    {
        var weather = await weatherService.GetWeatherForCityAsync(city);
        Console.WriteLine(weather.description);
        Console.WriteLine(JsonSerializer.Serialize(weather, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("Did you see a big json file? Then it worked :-)");
        Console.ReadLine();
    });
    app.AddCommand("ws", (string city, IWeatherService weatherService) =>
    {
        var weather = weatherService.GetWeatherForCity(city);
        Console.WriteLine(weather.description);
        Console.WriteLine(JsonSerializer.Serialize(weather, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("Did you see a big json file? Then it worked :-)");
        Console.ReadLine();
    });
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}



