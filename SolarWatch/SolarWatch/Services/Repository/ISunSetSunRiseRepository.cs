using SolarWatch.Model;

namespace SolarWatch.Services.Repository;

public interface ISunSetSunRiseRepository
{
    Task<IEnumerable<SunSetSunRiseResponse>> GetAll();
    Task Add(SunSetSunRiseResponse city);
    Task Delete(SunSetSunRiseResponse city);
    Task Update(SunSetSunRiseResponse city);
}