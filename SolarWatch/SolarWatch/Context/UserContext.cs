using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SolarWatch.Context;

public class UserContext : IdentityDbContext<IdentityUser, IdentityRole, string>
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=SolarWatch;User Id=sa;Password=Feher2023vendeL!;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}