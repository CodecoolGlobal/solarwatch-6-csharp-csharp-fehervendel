using SolarWatch.Model;

namespace SolarWatch.Services;

public interface ISolarWatchJsonProcessor
{
    Task<SunSetSunRiseResponse> Process(string data);
}