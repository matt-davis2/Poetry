// PoetryApp/Services/WeatherApiService.cs
using PoetryApp.Controllers;

namespace PoetryApp.Services;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<WeatherForecast[]?> GetWeatherForecastAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>("api/weather");
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching weather data from API");
            return null;
        }
    }

    public async Task<WeatherForecast[]?> GetWeatherForecastAsync(int days)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<WeatherForecast[]>($"api/weather/{days}");
            return response;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching weather data from API");
            return null;
        }
    }
}