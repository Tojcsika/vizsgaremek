using Vizsgaremek.Dtos;

namespace Vizsgaremek.Services.Interfaces
{
    public interface IShelfProductService
    {
        Task<IEnumerable<ShelfProductDto>> GetAllShelfProductsAsync();
        Task<ShelfProductDto?> GetShelfProductByIdAsync(int id);
        Task<int> CreateShelfProductAsync(ShelfProductDto shelfProductDto);
        Task<bool> UpdateShelfProductAsync(int id, ShelfProductDto shelfProductDto);
        Task<bool> DeleteShelfProductAsync(int id);
    }
}
