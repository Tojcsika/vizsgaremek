using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Services
{
    public class StorageRackService : IStorageRackService
    {
        private readonly IStorageRackRepository _storageRackRepository;

        public StorageRackService(IStorageRackRepository storageRackRepository)
        {
            _storageRackRepository = storageRackRepository;
        }

        public async Task<IEnumerable<StorageRackDto>> GetAllStorageRacksAsync()
        {
            var storageRacks = await _storageRackRepository.GetAllStorageRacksAsync();

            return storageRacks.Select(storageRack => new StorageRackDto()
            {
                Id = storageRack.Id,
                StorageId = storageRack.StorageId,
                Row = storageRack.Row,
                RowPosition = storageRack.RowPosition,
                WeightLimit = storageRack.WeightLimit,
                Shelves = storageRack.Shelves.Count
            }).ToList();
        }

        public async Task<StorageRackDto?> GetStorageRackByIdAsync(int id)
        {
            var storageRack = await _storageRackRepository.GetStorageRackByIdAsync(id);

            if (storageRack == null)
            {
                return null;
            }

            return new StorageRackDto()
            {
                Id = storageRack.Id,
                StorageId = storageRack.StorageId,
                Row = storageRack.Row,
                RowPosition = storageRack.RowPosition,
                WeightLimit = storageRack.WeightLimit,
                Shelves = storageRack.Shelves.Count
            };
        }

        public async Task<int> CreateStorageRackAsync(StorageRackDto storageRackDto)
        {
            var newStorageRack = new StorageRack()
            {
                Id = storageRackDto.Id,
                StorageId = storageRackDto.StorageId,
                Row = storageRackDto.Row,
                RowPosition = storageRackDto.RowPosition,
                WeightLimit = storageRackDto.WeightLimit,
            };

            await _storageRackRepository.CreateStorageRackAsync(newStorageRack);

            return newStorageRack.Id;
        }

        public async Task<bool> UpdateStorageRackAsync(int id, StorageRackDto storageRackDto)
        {
            var existingStorageRack = await _storageRackRepository.GetStorageRackByIdAsync(id);
            if (existingStorageRack == null)
            {
                return false;
            }
            existingStorageRack.Row = storageRackDto.Row;
            existingStorageRack.RowPosition = storageRackDto.RowPosition;
            existingStorageRack.WeightLimit = storageRackDto.WeightLimit;

            await _storageRackRepository.UpdateStorageRackAsync(existingStorageRack);
            return true;
        }

        public async Task<bool> DeleteStorageRackAsync(int id)
        {
            var existingStorage = await _storageRackRepository.GetStorageRackByIdAsync(id);
            if (existingStorage == null)
            {
                return false;
            }
            await _storageRackRepository.DeleteStorageRackAsync(existingStorage);
            return true;
        }

        public async Task<IEnumerable<ShelfDto>> GetShelvesAsync(int storageRackId)
        {
            var storage = await _storageRackRepository.GetStorageRackByIdAsync(storageRackId);

            if (storage == null)
            {
                return new List<ShelfDto>();
            }

            return storage.Shelves.Select(shelf => new ShelfDto()
            {
                Id = shelf.Id,
                StorageRackId = shelf.StorageRackId,
                Level = shelf.Level,
                Width = shelf.Width,
                Length = shelf.Length,
                Height = shelf.Height,
                WeightLimit = shelf.WeightLimit,
                TotalProducts = shelf.ShelfProducts?.Sum(sp => sp.Quantity) ?? 0
            }).ToList();
        }
    }
}
