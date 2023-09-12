namespace SolarWatch.Services;

public interface ICityDataProvider
{
    Task<string> GetCityData(string cityName);
}