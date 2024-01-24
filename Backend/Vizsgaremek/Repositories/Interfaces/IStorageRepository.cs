using Vizsgaremek.Entities;

namespace Vizsgaremek.Repositories.Interfaces
{
    public interface IStorageRepository
    {
        Task<IEnumerable<Storage>> GetAllStoragesAsync();
        Task<Storage?> GetStorageByIdAsync(int id);
        Task CreateStorageAsync(Storage storage);
        Task UpdateStorageAsync(Storage storage);
        Task DeleteStorageAsync(Storage storage);
    }
}
