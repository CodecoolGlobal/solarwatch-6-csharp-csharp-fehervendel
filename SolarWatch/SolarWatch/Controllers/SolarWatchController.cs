using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolarWatch.Model;
using SolarWatch.Services;
using SolarWatch.Services.Repository;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class SolarWatchController : ControllerBase
{
    private readonly ILogger<SolarWatchController> _logger;
    private ISolarWatchDataProvider _solarWatchDataProvider;
    private ISolarWatchJsonProcessor _solarWatchJsonProcessor;
    private ICityRepository _cityRepository;
    private ISunSetSunRiseRepository _sunSetSunRiseRepository;
    private IJsonProcessor _jsonProcessor;
    private ICityDataProvider _cityDataProvider;

    public SolarWatchController(ILogger<SolarWatchController> logger, ISolarWatchDataProvider solarWatchDataProvider, ISolarWatchJsonProcessor solarWatchJsonProcessor, ICityRepository cityRepository, ISunSetSunRiseRepository sunSetSunRiseRepository,
    IJsonProcessor jsonProcessor, ICityDataProvider cityDataProvider)
    {
        _logger = logger;
        _solarWatchDataProvider = solarWatchDataProvider;
        _solarWatchJsonProcessor = solarWatchJsonProcessor;
        _cityRepository = cityRepository;
        _sunSetSunRiseRepository = sunSetSunRiseRepository;
        _jsonProcessor = jsonProcessor;
        _cityDataProvider = cityDataProvider;
    }

    [HttpGet("GetByName"), Authorize(Roles="Admin")]
    public async Task<ActionResult<WeatherForecast>> GetByNameAndDate([Required] string cityName)
    {
        var city = await _cityRepository.GetByName(cityName);
        
        try
        {
            if(city == null)
            {
              var data = await _solarWatchDataProvider.GetSunSetSunRiseResponse(cityName);
              var result = await _solarWatchJsonProcessor.Process(data); //Set sunrise, sunset
              
              var sunSetResult = await _cityDataProvider.GetCityData(cityName);
              var resultCity = await _jsonProcessor.Process(sunSetResult);
              result.city = resultCity;
              
              var addToSunRiseDataBase = _sunSetSunRiseRepository.Add(result); 
              var addToCityDataBase = _cityRepository.Add(resultCity);
              
              return Ok(resultCity);
            }
            
            return Ok(city);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting sunset-sunrise data");
            return NotFound("Error getting weather data");
        }
    }
}