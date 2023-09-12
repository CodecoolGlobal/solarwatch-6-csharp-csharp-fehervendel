using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SolarWatch;
using SolarWatch.Controllers;
using SolarWatch.Model;
using SolarWatch.Services;

namespace SolarWatchTest;

[TestFixture]
public class SolarWatchTests
{
    private Mock<ILogger<SolarWatchController>> _loggerMock;
    private Mock<ISolarWatchJsonProcessor> _solarWatchJsonProcessorMock;
    private Mock<ISolarWatchDataProvider> _solarWatchDataProviderMock;
    private SolarWatchController _solarWatchController;
    
    [SetUp]
    public void SetUp()
    {
        _loggerMock = new Mock<ILogger<SolarWatchController>>();
        _solarWatchJsonProcessorMock = new Mock<ISolarWatchJsonProcessor>();
        _solarWatchDataProviderMock = new Mock<ISolarWatchDataProvider>();
        _solarWatchController = new SolarWatchController(_loggerMock.Object, _solarWatchDataProviderMock.Object, _solarWatchJsonProcessorMock.Object);
    }

    [Test]
    public async Task GetByNameAndDateReturnsNotFoundIfLatLonProviderFails()
    {
        
        _solarWatchDataProviderMock.Setup(x => x.GetSunSetSunRiseResponse(It.IsAny<string>(), It.IsAny<DateTime>()))
            .Throws(new Exception());

        var resultTask = _solarWatchController.GetByNameAndDate("kiskutya", new DateTime(2022,01,01));
        var result = await resultTask;
        
        Assert.IsInstanceOf(typeof(NotFoundObjectResult), result);
    }
}