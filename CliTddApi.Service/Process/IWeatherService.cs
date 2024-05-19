using CliTddApi.Service.Entity;

namespace CliTddApi.Service.Process
{
    public interface IWeatherService
    {
        Task<WeatherResponse?> GetWeatherForCityAsync(string city);

        WeatherResponse? GetWeatherForCity(string city);
    }
}









