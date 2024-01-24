using Vizsgaremek.Dtos;

namespace Vizsgaremek.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<int> CreateProductAsync(ProductDto productDto);
        Task<bool> UpdateProductAsync(int id, ProductDto productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductShelfDto>> GetProductShelvesAsync(int productId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchString);
    }
}
