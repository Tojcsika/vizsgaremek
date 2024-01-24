using Vizsgaremek.Entities;

namespace Vizsgaremek.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task CreateProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<IEnumerable<Product>> SearchProductsAsync(string searchString);
    }
}
