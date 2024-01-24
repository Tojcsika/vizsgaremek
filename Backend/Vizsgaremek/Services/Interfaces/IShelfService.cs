using Vizsgaremek.Dtos;

namespace Vizsgaremek.Services.Interfaces
{
    public interface IShelfService
    {
        Task<IEnumerable<ShelfDto>> GetAllShelvesAsync();
        Task<ShelfDto?> GetShelfByIdAsync(int id);
        Task<int> CreateShelfAsync(ShelfDto shelfDto);
        Task<bool> UpdateShelfAsync(int id, ShelfDto shelfDto);
        Task<bool> DeleteShelfAsync(int id);
    }
}
