using Vizsgaremek.Entities;

namespace Vizsgaremek.Repositories.Interfaces
{
    public interface IStorageRackRepository
    {
        Task<IEnumerable<StorageRack>> GetAllStorageRacksAsync();
        Task<StorageRack?> GetStorageRackByIdAsync(int id);
        Task CreateStorageRackAsync(StorageRack storageRack);
        Task UpdateStorageRackAsync(StorageRack storageRack);
        Task DeleteStorageRackAsync(StorageRack storageRack);
    }
}
