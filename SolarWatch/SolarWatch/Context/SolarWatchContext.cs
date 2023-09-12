using Microsoft.EntityFrameworkCore;
using SolarWatch.Model;

namespace SolarWatch.Context;

public class SolarWatchContext : DbContext
{
    public DbSet<City> Cities { get; set; }
    public DbSet<SunSetSunRiseResponse> SunsetSunrise {get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SolarWatch;User Id=sa;Password=Feher2023vendeL!;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<City>()
            .HasOne(c => c.SunRiseResponse)
            .WithOne(s => s.city)
            .HasForeignKey<SunSetSunRiseResponse>(ss => ss.CityId);

        builder.Entity<City>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}