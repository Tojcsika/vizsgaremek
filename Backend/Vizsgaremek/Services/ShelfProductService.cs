using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Services
{
    public class ShelfProductService : IShelfProductService
    {
        private readonly IShelfProductRepository _shelfProductRepository;

        public ShelfProductService(IShelfProductRepository shelfProductRepository)
        {
            _shelfProductRepository = shelfProductRepository;
        }

        public async Task<IEnumerable<ShelfProductDto>> GetAllShelfProductsAsync()
        {
            var shelfProducts = await _shelfProductRepository.GetAllShelfProductsAsync();

            return shelfProducts.Select(sp => new ShelfProductDto
            {
                Id = sp.Id,
                Name = sp.Product.Name,
                Weight = sp.Product?.Weight ?? 0,
                ProductId = sp.ProductId,
                ShelfId = sp.ShelfId,
                Quantity = sp.Quantity,
                TotalWeight = (sp.Product?.Weight ?? 0) * sp.Quantity,
                Width = sp.Width,
                Length = sp.Length,
                Height = sp.Height
            }).ToList();
        }

        public async Task<ShelfProductDto?> GetShelfProductByIdAsync(int id)
        {
            var shelfProduct = await _shelfProductRepository.GetShelfProductByIdAsync(id);

            if (shelfProduct == null)
            {
                return null;
            }

            return new ShelfProductDto
            {
                Id = shelfProduct.Id,
                Name = shelfProduct.Product.Name,
                Weight = shelfProduct.Product?.Weight ?? 0,
                ProductId = shelfProduct.ProductId,
                ShelfId = shelfProduct.ShelfId,
                Quantity = shelfProduct.Quantity,
                TotalWeight = (shelfProduct.Product?.Weight ?? 0) * shelfProduct.Quantity,
                Width = shelfProduct.Width,
                Length = shelfProduct.Length,
                Height = shelfProduct.Height
            };
        }

        public async Task<int> CreateShelfProductAsync(ShelfProductDto shelfProductDto)
        {
            var newShelfProduct = new ShelfProduct
            {
                ProductId = shelfProductDto.ProductId,
                ShelfId = shelfProductDto.ShelfId,
                Quantity = shelfProductDto.Quantity,
                Width = shelfProductDto.Width,
                Length = shelfProductDto.Length,
                Height = shelfProductDto.Height
            };

            await _shelfProductRepository.CreateShelfProductAsync(newShelfProduct);
            return newShelfProduct.Id;
        }

        public async Task<bool> UpdateShelfProductAsync(int id, ShelfProductDto shelfProductDto)
        {
            var existingShelfProduct = await _shelfProductRepository.GetShelfProductByIdAsync(id);
            if (existingShelfProduct == null)
            {
                return false;
            }

            existingShelfProduct.Quantity = shelfProductDto.Quantity;
            existingShelfProduct.Width = shelfProductDto.Width;
            existingShelfProduct.Length = shelfProductDto.Length;
            existingShelfProduct.Height = shelfProductDto.Height;

            await _shelfProductRepository.UpdateShelfProductAsync(existingShelfProduct);
            return true;
        }

        public async Task<bool> DeleteShelfProductAsync(int id)
        {
            var existingShelfProduct = await _shelfProductRepository.GetShelfProductByIdAsync(id);
            if (existingShelfProduct == null)
            {
                return false;
            }

            await _shelfProductRepository.DeleteShelfProductAsync(existingShelfProduct);
            return true;
        }
    }
}
