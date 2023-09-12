using System.Net;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class SolarWatchDataProvider : ISolarWatchDataProvider
{
    private IJsonProcessor _jsonProcessor;
    private ICityDataProvider _cityDataProvider;
    private readonly ILogger<SolarWatchDataProvider> _logger;

    public SolarWatchDataProvider(IJsonProcessor jsonProcessor, ICityDataProvider cityDataProvider,ILogger<SolarWatchDataProvider> logger)
    {
        _jsonProcessor = jsonProcessor;
        _cityDataProvider = cityDataProvider;
        _logger = logger;
    }

    public SolarWatchDataProvider()
    {
        
    }
    public async Task<string> GetSunSetSunRiseResponse(string city)
    {
       var data = await _cityDataProvider.GetCityData(city);
       City response = await _jsonProcessor.Process(data);

       var url = $"https://api.sunrise-sunset.org/json?lat={response.Lat}&lng={response.Lon}";

       var client = new HttpClient();
       
       _logger.LogInformation("Calling Sunrise-Sunset API from {url}", url);

       var result = await client.GetAsync(url);

       return await result.Content.ReadAsStringAsync();
    }
}