using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;

namespace Vizsgaremek.Repositories
{
    public class StorageRackRepository : IStorageRackRepository
    {
        private readonly DatabaseContext _dbContext;

        public StorageRackRepository(DatabaseContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<StorageRack>> GetAllStorageRacksAsync()
        {
            return await _dbContext.StorageRacks.ToListAsync();
        }

        public async Task<StorageRack?> GetStorageRackByIdAsync(int id)
        {
            return await _dbContext.StorageRacks.FindAsync(id);
        }

        public async Task CreateStorageRackAsync(StorageRack storageRack)
        {
            _dbContext.Add(storageRack);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateStorageRackAsync(StorageRack storageRack)
        {
            _dbContext.Entry(storageRack).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteStorageRackAsync(StorageRack storageRack)
        {
            _dbContext.StorageRacks.Remove(storageRack);
            await _dbContext.SaveChangesAsync();
        }
    }

}
