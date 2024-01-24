using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;

namespace Vizsgaremek.Repositories
{
    public class ShelfProductRepository : IShelfProductRepository
    {
        private readonly DatabaseContext _dbContext;

        public ShelfProductRepository(DatabaseContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<ShelfProduct>> GetAllShelfProductsAsync()
        {
            return await _dbContext.ShelfProducts.ToListAsync();
        }

        public async Task<ShelfProduct?> GetShelfProductByIdAsync(int id)
        {
            return await _dbContext.ShelfProducts.FindAsync(id);
        }

        public async Task<IEnumerable<ShelfProduct>> GetShelfProductsByProductId(int productId)
        {
            return await _dbContext.ShelfProducts.Where(sp => sp.ProductId == productId).ToListAsync();
        }

        public async Task DeleteShelfProductAsync(ShelfProduct shelfProduct)
        {
            _dbContext.ShelfProducts.Remove(shelfProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateShelfProductAsync(ShelfProduct newShelfProduct)
        {
            _dbContext.Add(newShelfProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateShelfProductAsync(ShelfProduct existingShelfProduct)
        {
            _dbContext.Entry(existingShelfProduct).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}
