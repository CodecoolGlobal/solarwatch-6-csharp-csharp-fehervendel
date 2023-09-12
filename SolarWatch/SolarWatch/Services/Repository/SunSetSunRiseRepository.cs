using Microsoft.EntityFrameworkCore;
using SolarWatch.Context;
using SolarWatch.Model;

namespace SolarWatch.Services.Repository;

public class SunSetSunRiseRepository : ISunSetSunRiseRepository
{
    public async Task<IEnumerable<SunSetSunRiseResponse>> GetAll()
    {
        using var dbContext = new SolarWatchContext();
        return await dbContext.SunsetSunrise.ToListAsync();
    }
    
    public async Task Add(SunSetSunRiseResponse sunrise)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Add(sunrise);
        await dbContext.SaveChangesAsync();
    }

    public async Task Delete(SunSetSunRiseResponse sunrise)
    {
        using var dbContext = new SolarWatchContext();
        dbContext.Remove(sunrise);
         await dbContext.SaveChangesAsync();
    }

    public async Task Update(SunSetSunRiseResponse sunrise)
    {  
        using var dbContext = new SolarWatchContext();
        dbContext.Update(sunrise);
        await dbContext.SaveChangesAsync();
    }
}