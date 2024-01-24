using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;

namespace Vizsgaremek.Repositories
{
    public class StorageRepository : IStorageRepository
    {
        private readonly DatabaseContext _dbContext;

        public StorageRepository(DatabaseContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Storage>> GetAllStoragesAsync()
        {
            return await _dbContext.Storages.ToListAsync();
        }

        public async Task<Storage?> GetStorageByIdAsync(int id)
        {
            return await _dbContext.Storages.FindAsync(id);
        }

        public async Task CreateStorageAsync(Storage storage)
        {
            _dbContext.Add(storage);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStorageAsync(Storage storage)
        {
            _dbContext.Entry(storage).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStorageAsync(Storage storage)
        {
            _dbContext.Storages.Remove(storage);
            await _dbContext.SaveChangesAsync();
        }
    }
}
