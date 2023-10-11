using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using SolarWatch;
using SolarWatch.Controllers;
using SolarWatch.Model;
using SolarWatch.Services;
using SolarWatch.Services.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;


namespace SolarWatchTest;

[TestFixture]
public class SolarWatchTests : WebApplicationFactory<Program>
{
    private Mock<ILogger<SolarWatchController>> _loggerMock;
    private Mock<ISolarWatchJsonProcessor> _solarWatchJsonProcessorMock;
    private Mock<ISolarWatchDataProvider> _solarWatchDataProviderMock;
    private SolarWatchController _solarWatchController;
    private Mock<ICityRepository> _cityRepositoryMock;
    private Mock<ISunSetSunRiseRepository> _sunSetSunRiseRepositoryMock;
    private Mock<IJsonProcessor> _jsonProcessorMock;
    private Mock<ICityDataProvider> _cityDataProviderMock;

    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<SolarWatchController>>();
        _solarWatchJsonProcessorMock = new Mock<ISolarWatchJsonProcessor>();
        _solarWatchDataProviderMock = new Mock<ISolarWatchDataProvider>();
        _cityRepositoryMock = new Mock<ICityRepository>();
        _sunSetSunRiseRepositoryMock = new Mock<ISunSetSunRiseRepository>();
        _jsonProcessorMock = new Mock<IJsonProcessor>();
        _cityDataProviderMock = new Mock<ICityDataProvider>();
        
        _solarWatchController = new SolarWatchController(_loggerMock.Object, _solarWatchDataProviderMock.Object, _solarWatchJsonProcessorMock.Object,
            _cityRepositoryMock.Object, _sunSetSunRiseRepositoryMock.Object,
            _jsonProcessorMock.Object, _cityDataProviderMock.Object);
    }
    
    [Test]
    public async Task GetByName_ReturnsNotFound_WhenCityDoesNotExist()
    {
        string cityName = "NonExistentCity";

        _cityRepositoryMock.Setup(repo => repo.GetByName(cityName))
            .ReturnsAsync((City)null);
        
        var result = await _solarWatchController.GetByName(cityName);
        
        Assert.IsInstanceOf(typeof(NotFoundObjectResult), result.Result);
        var notFoundResult = (NotFoundObjectResult)result.Result;
        Assert.That(notFoundResult.Value, Is.EqualTo("Error getting weather data"));
    }
    private HttpClient _client;

        [OneTimeSetUp]
        public void Setup()
        {
            _client = CreateClient();
            _client.DefaultRequestHeaders.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJUb2tlbkZvclRoZUFwaVdpdGhBdXRoIiwianRpIjoiZmNiZDQzZGItMDRhZi00NWI5LTgyYTktZTQ2MjAwNDRlMzk5IiwiaWF0IjoiMDkvMjkvMjAyMyAwOToyNjozOSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiZDMyYzBiMTYtZjY5Yi00ZThmLTgzNjQtN2I0MzIzMzFjNGEyIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6ImFkbWluIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiYWRtaW5AYWRtaW4uY29tIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiQWRtaW4iLCJleHAiOjE2OTU5ODEzOTksImlzcyI6ImFwaVdpdGhBdXRoQmFja2VuZCIsImF1ZCI6ImFwaVdpdGhBdXRoQmFja2VuZCJ9.YG2FAmByiReZwhWBd3YwxcIMtC2DBROvQjwfV4lnEZU");

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

        [Test]
        public async Task TestGetByNameEndpoint_ExistingCity_ShouldReturnOk()
        {
            var cityName = "Moscow";
            
            var response = await _client.GetAsync($"/SolarWatch/GetByName?cityName={cityName}");
            
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<City>(responseContent);
            
            Assert.IsNotNull(result);
            Assert.AreEqual(cityName, result.Name);
           
        }

        [Test]
        public async Task TestGetByNameEndpoint_NonExistingCity_ShouldReturnNotFound()
        {
            var cityName = "aaaaaaaaaaa"; 
            
            var response = await _client.GetAsync($"/SolarWatch/GetByName?cityName={cityName}");
            
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.AreEqual("Error getting weather data", responseContent);
        }
    
}