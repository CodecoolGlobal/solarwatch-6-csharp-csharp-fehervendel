using SolarWatch.Model;

namespace SolarWatch.Services;

public interface ISolarWatchDataProvider
{
    Task<string> GetSunSetSunRiseResponse(string city);
}