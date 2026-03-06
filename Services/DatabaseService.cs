using BeerRate_MAUI_App.Data;
using Microsoft.EntityFrameworkCore;

namespace BeerRate_MAUI_App.Services
{
    public class DatabaseService
    {
        private readonly AppDbContext _context;

        public DatabaseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task InitializeAsync()
        {
            // Ensure database exists
            await _context.Database.EnsureCreatedAsync();
            
            try
            {
                // Try to check if Notes column exists by querying
                var testQuery = await _context.BeerRatings.FirstOrDefaultAsync();
            }
            catch (Microsoft.Data.Sqlite.SqliteException ex) when (ex.Message.Contains("no such column: b.Notes"))
            {
                // Notes column doesn't exist - add it
                await _context.Database.ExecuteSqlRawAsync(
                    "ALTER TABLE BeerRatings ADD COLUMN Notes TEXT NULL");
            }
        }

        public AppDbContext GetContext() => _context;
    }
}
