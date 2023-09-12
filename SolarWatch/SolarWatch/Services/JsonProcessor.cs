using System.Text.Json;
using SolarWatch.Model;

namespace SolarWatch.Services;

public class JsonProcessor : IJsonProcessor
{
    public async Task<City> Process(string data)
    {
        JsonDocument json = JsonDocument.Parse(data);
        JsonElement root = json.RootElement;

        JsonElement firstElement = root[0];


        City city = new City
        {
            Lat = firstElement.GetProperty("lat").GetDouble(),
            Lon = firstElement.GetProperty("lon").GetDouble(),
            Name = firstElement.GetProperty("name").GetString(),
            State = firstElement.GetProperty("state").GetString(),
            Country = firstElement.GetProperty("country").GetString()
        };

        return city;
    }
}