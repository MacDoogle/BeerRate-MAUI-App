using Microsoft.EntityFrameworkCore;
using BeerRate_MAUI_App.Models;

namespace BeerRate_MAUI_App.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<BeerRating> BeerRatings { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "beerratings.db");
                optionsBuilder.UseSqlite($"Filename={dbPath}");
            }
        }
    }
}
