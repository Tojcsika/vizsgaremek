using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Services
{
    public class ShelfService : IShelfService
    {
        private readonly IShelfRepository _shelfRepository;

        public ShelfService(IShelfRepository shelfRepository)
        {
            _shelfRepository = shelfRepository;
        }

        public async Task<IEnumerable<ShelfDto>> GetAllShelvesAsync()
        {
            var shelves = await _shelfRepository.GetAllShelvesAsync();

            return shelves.Select(shelf => new ShelfDto
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

        public async Task<ShelfDto?> GetShelfByIdAsync(int id)
        {
            var shelf = await _shelfRepository.GetShelfByIdAsync(id);
            if (shelf == null)
            {
                return null;
            }

            var shelfProducts = shelf.ShelfProducts.Select(sp => new ShelfProductDto
            {
                Id = sp.Id,
                Name = sp.Product.Name,
                Weight = sp.Product?.Weight ?? 0,
                Quantity = sp.Quantity,
                TotalWeight = (sp.Product?.Weight ?? 0) * sp.Quantity,
                Width = sp.Width,
                Length = sp.Length,
                Height = sp.Height
            }).ToList();

            return new ShelfDto
            {
                Id = shelf.Id,
                StorageRackId = shelf.StorageRackId,
                Level = shelf.Level,
                Width = shelf.Width,
                Length = shelf.Length,
                Height = shelf.Height,
                WeightLimit = shelf.WeightLimit,
                TotalProducts = shelf.ShelfProducts.Sum(sp => sp.Quantity),
                ShelfProducts = shelfProducts
            };
        }

        public async Task<int> CreateShelfAsync(ShelfDto shelfDto)
        {
            var newShelf = new Shelf
            {
                StorageRackId = shelfDto.StorageRackId,
                Level = shelfDto.Level,
                Width = shelfDto.Width,
                Length = shelfDto.Length,
                Height = shelfDto.Height,
                WeightLimit = shelfDto.WeightLimit,
            };

            await _shelfRepository.CreateShelfAsync(newShelf);

            return newShelf.Id;
        }

        public async Task<bool> UpdateShelfAsync(int id, ShelfDto shelfDto)
        {
            var existingShelf = await _shelfRepository.GetShelfByIdAsync(id);
            if (existingShelf == null)
            {
                return false;
            }

            existingShelf.Level = shelfDto.Level;
            existingShelf.Width = shelfDto.Width;
            existingShelf.Length = shelfDto.Length;
            existingShelf.Height = shelfDto.Height;
            existingShelf.WeightLimit = shelfDto.WeightLimit;

            await _shelfRepository.UpdateShelfAsync(existingShelf);
            return true;
        }

        public async Task<bool> DeleteShelfAsync(int id)
        {
            var existingShelf = await _shelfRepository.GetShelfByIdAsync(id);
            if (existingShelf == null)
            {
                return false;
            }
            await _shelfRepository.DeleteShelfAsync(existingShelf);
            return true;
        }
    }
}
