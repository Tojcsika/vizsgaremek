using Vizsgaremek.Dtos;

namespace Vizsgaremek.Services.Interfaces
{
    public interface IStorageService
    {
        Task<IEnumerable<StorageDto>> GetAllStoragesAsync();
        Task<StorageDto?> GetStorageByIdAsync(int id);
        Task<int> CreateStorageAsync(StorageDto storageDto);
        Task<bool> UpdateStorageAsync(int id, StorageDto storageDto); 
        Task<bool> DeleteStorageAsync(int id);
        Task<IEnumerable<StorageRackDto>> GetStorageRacksAsync(int storageId);
    }
}
