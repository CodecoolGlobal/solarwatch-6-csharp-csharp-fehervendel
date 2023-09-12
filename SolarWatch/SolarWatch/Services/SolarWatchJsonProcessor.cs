using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class SolarWatchJsonProcessor : ISolarWatchJsonProcessor
{
    public async Task<SunSetSunRiseResponse> Process(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement root = json.RootElement;

        JsonElement results = root.GetProperty("results");

        SunSetSunRiseResponse response = new SunSetSunRiseResponse
        {
            Sunrise = results.GetProperty("sunrise").GetString(),
            Sunset = results.GetProperty("sunset").GetString()
        };

        return response;
    }
}