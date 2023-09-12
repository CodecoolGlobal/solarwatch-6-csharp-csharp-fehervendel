using System.Net;

namespace SolarWatch.Services;

public class CityDataProvider : ICityDataProvider
{
    
    private readonly ILogger<CityDataProvider> _logger;

    public CityDataProvider(ILogger<CityDataProvider> logger)
    {
        _logger = logger;
    }
    
    public async Task<string> GetCityData(string cityName)
    {
        var apiKey = "81801e606480c0782a26ad943cc4a746";

        var url =
            $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&limit=1&appid=81801e606480c0782a26ad943cc4a746";

        var client = new HttpClient();
        _logger.LogInformation("Calling Geocoding API with: {url}", url);
        var result = await client.GetAsync(url);
        var resultAsString = await result.Content.ReadAsStringAsync();

        if (resultAsString == "[]")
        {
            throw new ArgumentException("City not found! Please provide a valid city name.");
        }
        
        return resultAsString;
    }
}