using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatch.Controllers;

[ApiController]
[Route("[controller]")]
public class GeoCodingController : ControllerBase
{
    private readonly ILogger<GeoCodingController> _logger;
    private IJsonProcessor _jsonProcessor;
    private ICityDataProvider _cityDataProvider;

    public GeoCodingController(IJsonProcessor jsonProcessor, ICityDataProvider cityDataProvider, ILogger<GeoCodingController> logger)
    {
        _logger = logger;
        _jsonProcessor = jsonProcessor;
        _cityDataProvider = cityDataProvider;
    }

    [HttpGet, Authorize(Roles="User")]
    public async Task<IActionResult> GetLatAndLon(string cityName)
    {
        try
        {
            var data = await _cityDataProvider.GetCityData(cityName);
            return Ok(_jsonProcessor.Process(data));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting data");
            return NotFound("Error getting data");
        }
    }
}