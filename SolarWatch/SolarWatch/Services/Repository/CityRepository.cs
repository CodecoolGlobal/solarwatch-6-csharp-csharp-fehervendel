using Microsoft.EntityFrameworkCore;
using SolarWatch.Context;
using SolarWatch.Model;

namespace SolarWatch.Services.Repository;

public class CityRepository : ICityRepository
{
    public async Task<IEnumerable<City>> GetAll()
    {
        using var dbContext = new SolarWatchContext();
        return await dbContext.Cities.ToListAsync();
        
    }

    public async Task<City?> GetByName(string name)
    {
        using var dbContext = new SolarWatchContext();
        return await dbContext.Cities.Include(c => c.SunRiseResponse).FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task Add(City city)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Add(city);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(City city)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Remove(city);
        await dbContext.SaveChangesAsync();
    }

    public async Task Update(City city)
    {  
        using var dbContext = new SolarWatchContext();
        dbContext.Update(city);
        await dbContext.SaveChangesAsync();
    }
}