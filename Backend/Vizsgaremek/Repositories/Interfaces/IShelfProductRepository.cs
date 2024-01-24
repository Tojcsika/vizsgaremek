using Vizsgaremek.Entities;

namespace Vizsgaremek.Repositories.Interfaces
{
    public interface IShelfProductRepository
    {
        Task<IEnumerable<ShelfProduct>> GetAllShelfProductsAsync();
        Task<ShelfProduct?> GetShelfProductByIdAsync(int id);
        Task CreateShelfProductAsync(ShelfProduct newShelfProduct);
        Task UpdateShelfProductAsync(ShelfProduct existingShelfProduct);
        Task DeleteShelfProductAsync(ShelfProduct shelfProduct);
        Task<IEnumerable<ShelfProduct>> GetShelfProductsByProductId(int productId);
    }
}
