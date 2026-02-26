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
            await _context.Database.EnsureCreatedAsync();
        }

        public AppDbContext GetContext() => _context;
    }
}
