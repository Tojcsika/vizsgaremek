using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Services
{
    public class StorageService : IStorageService
    {
        private readonly IStorageRepository _storageRepository;

        public StorageService(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }

        public async Task<IEnumerable<StorageDto>> GetAllStoragesAsync()
        {
            var storages = await _storageRepository.GetAllStoragesAsync();

            return storages.Select(storage => new StorageDto
            {
                Id = storage.Id,
                Name = storage.Name,
                Address = storage.Address,
                Area = storage.Area,
                TotalStorageRacks = storage.StorageRacks?.Count ?? 0,
                TotalProducts = CalculateTotalProducts(storage)
            }).ToList();
        }

        public async Task<StorageDto?> GetStorageByIdAsync(int id)
        {
            var storage = await _storageRepository.GetStorageByIdAsync(id);

            if (storage == null)
            {
                return null;
            }

            return new StorageDto
            {
                Id = storage.Id,
                Name = storage.Name,
                Address = storage.Address,
                Area = storage.Area,
                TotalStorageRacks = storage.StorageRacks?.Count ?? 0,
                TotalProducts = CalculateTotalProducts(storage)
            };
        }

        public async Task<int> CreateStorageAsync(StorageDto storageDto)
        {
            var newStorage = new Storage
            {
                Name = storageDto.Name,
                Address = storageDto.Address,
                Area = storageDto.Area
            };

            await _storageRepository.CreateStorageAsync(newStorage);

            return newStorage.Id;
        }

        public async Task<bool> UpdateStorageAsync(int id, StorageDto storageDto)
        {
            var existingStorage = await _storageRepository.GetStorageByIdAsync(id);
            if (existingStorage == null)
            {
                return false;
            }

            existingStorage.Name = storageDto.Name;
            existingStorage.Address = storageDto.Address;
            existingStorage.Area = storageDto.Area;

            await _storageRepository.UpdateStorageAsync(existingStorage);
            return true;
        }

        public async Task<bool> DeleteStorageAsync(int id)
        {
            var existingStorage = await _storageRepository.GetStorageByIdAsync(id);
            if (existingStorage == null)
            {
                return false;
            }
            await _storageRepository.DeleteStorageAsync(existingStorage);
            return true;
        }

        public async Task<IEnumerable<StorageRackDto>> GetStorageRacksAsync(int storageId)
        {
            var storage = await _storageRepository.GetStorageByIdAsync(storageId);

            if (storage == null)
            {
                return new List<StorageRackDto>();
            }

            return storage.StorageRacks.Select(storageRack => new StorageRackDto()
            {
                Id = storageRack.Id,
                StorageId = storageRack.StorageId,
                Row = storageRack.Row,
                RowPosition = storageRack.RowPosition,
                WeightLimit = storageRack.WeightLimit,
                Shelves = storageRack.Shelves.Count
            }).ToList();
        }

        private int CalculateTotalProducts(Storage storage)
        {
            return storage.StorageRacks?.SelectMany(sr => sr.Shelves).Sum(s => s.ShelfProducts.Sum(sp => sp.Quantity)) ?? 0;
        }
    }
}
