using SolarWatch.Model;

namespace SolarWatch.Services;

public interface IJsonProcessor
{
    Task<City> Process(string data);
}