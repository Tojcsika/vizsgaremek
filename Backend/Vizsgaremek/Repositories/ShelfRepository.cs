using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;

namespace Vizsgaremek.Repositories
{
    public class ShelfRepository : IShelfRepository
    {
        private readonly DatabaseContext _dbContext;

        public ShelfRepository(DatabaseContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Shelf>> GetAllShelvesAsync()
        {
            return await _dbContext.Shelves.ToListAsync();
        }

        public async Task<Shelf?> GetShelfByIdAsync(int id)
        {
            return await _dbContext.Shelves.FindAsync(id);
        }

        public async Task CreateShelfAsync(Shelf shelf)
        {
            _dbContext.Add(shelf);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateShelfAsync(Shelf shelf)
        {
            _dbContext.Entry(shelf).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteShelfAsync(Shelf shelf)
        {
            _dbContext.Shelves.Remove(shelf);
            await _dbContext.SaveChangesAsync();
        }
    }
}
