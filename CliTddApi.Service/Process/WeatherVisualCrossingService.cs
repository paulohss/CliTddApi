using CliTddApi.Service.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliTddApi.Service.Process
{
    public class WeatherVisualCrossingService : IWeatherService
    {
        private string _apiKey;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherVisualCrossingService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _apiKey = Environment.GetEnvironmentVariable("weather_visual_crossing_api");
        }

        public async Task<WeatherResponse?> GetWeatherForCityAsync(string city)
        {
            Console.WriteLine("Client instance");
            HttpClient client = _httpClientFactory.CreateClient();
            var returnStr = "";

            var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?unitGroup=us&key={_apiKey}&contentType=json";

            Console.WriteLine("Client call");
            var response = await client.GetAsync(url);

            Console.WriteLine("Check response");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success Status Code");
                returnStr = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Json char count = {returnStr.Length}");

                Console.WriteLine("Starting Json Parsing");
                return JsonConvert.DeserializeObject<WeatherResponse>(returnStr);
            }
            else
            {
                Console.WriteLine($"Response failed with status code {response.StatusCode}");
            }

            return null;
        }

        public WeatherResponse? GetWeatherForCity(string city)
        {
            Console.WriteLine("Client instance");
            HttpClient client = _httpClientFactory.CreateClient();
            var returnStr = "";

            var url = $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/{city}?unitGroup=us&key={_apiKey}&contentType=json";

            Console.WriteLine("Client call");
            var response = client.GetAsync(url).Result;

            Console.WriteLine("Check response");
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Success Status Code");
                returnStr = response.Content.ReadAsStringAsync().Result;

                Console.WriteLine($"Json char count = {returnStr.Length}");

                Console.WriteLine("Starting Json Parsing");
                return JsonConvert.DeserializeObject<WeatherResponse>(returnStr);
            }
            else
            {
                Console.WriteLine($"Response failed with status code {response.StatusCode}");
            }

            return null;
        }
    }
}
