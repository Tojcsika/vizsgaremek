using Vizsgaremek.Dtos;

namespace Vizsgaremek.Services.Interfaces
{
    public interface IStorageRackService
    {
        Task<IEnumerable<StorageRackDto>> GetAllStorageRacksAsync();
        Task<StorageRackDto?> GetStorageRackByIdAsync(int id);
        Task<int> CreateStorageRackAsync(StorageRackDto storageRackDto);
        Task<bool> UpdateStorageRackAsync(int id, StorageRackDto storageRackDto);
        Task<bool> DeleteStorageRackAsync(int id);
        Task<IEnumerable<ShelfDto>> GetShelvesAsync(int storageRackId);
    }
}
