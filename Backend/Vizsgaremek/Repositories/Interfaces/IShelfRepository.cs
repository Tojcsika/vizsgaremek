using Vizsgaremek.Entities;

namespace Vizsgaremek.Repositories.Interfaces
{
    public interface IShelfRepository
    {
        Task<IEnumerable<Shelf>> GetAllShelvesAsync();
        Task<Shelf?> GetShelfByIdAsync(int id);
        Task CreateShelfAsync(Shelf shelf);
        Task UpdateShelfAsync(Shelf shelf);
        Task DeleteShelfAsync(Shelf shelf);
    }
}
