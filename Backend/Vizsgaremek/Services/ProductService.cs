using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories.Interfaces;
using Vizsgaremek.Services.Interfaces;

namespace Vizsgaremek.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IShelfProductRepository _shelfProductRepository;

        public ProductService(IProductRepository productRepository, IShelfProductRepository shelfProductRepository)
        {
            _productRepository = productRepository;
            _shelfProductRepository = shelfProductRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();

            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Width = product.Width,
                Length = product.Length,
                Height = product.Height,
                Weight = product.Weight,
                Description = product.Description
            }).ToList();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Width = product.Width,
                Length = product.Length,
                Height = product.Height,
                Weight = product.Weight,
                Description = product.Description
            };
        }

        public async Task<int> CreateProductAsync(ProductDto productDto)
        {
            var newProduct = new Product
            {
                Name = productDto.Name,
                Width = productDto.Width,
                Length = productDto.Length,
                Height = productDto.Height,
                Weight = productDto.Weight,
                Description = productDto.Description
            };

            await _productRepository.CreateProductAsync(newProduct);

            return newProduct.Id;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductDto productDto)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = productDto.Name;
            existingProduct.Width = productDto.Width;
            existingProduct.Length = productDto.Length;
            existingProduct.Height = productDto.Height;
            existingProduct.Weight = productDto.Weight;
            existingProduct.Description = productDto.Description;

            await _productRepository.UpdateProductAsync(existingProduct);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return false;
            }
            await _productRepository.DeleteProductAsync(existingProduct);
            return true;
        }

        public async Task<IEnumerable<ProductShelfDto>> GetProductShelvesAsync(int productId)
        {
            var productShelves = await _shelfProductRepository.GetShelfProductsByProductId(productId);

            return productShelves.Select(ps => new ProductShelfDto
            {
                Id = ps.Id,
                ProductId = ps.ProductId,
                ProductName = ps.Product.Name,
                ProductWeight = ps.Product.Weight,
                ShelfId = ps.ShelfId,
                ShelfProductQuantity = ps.Quantity,
                StorageName = ps.Shelf.StorageRack.Storage.Name,
                StorageRackRow = ps.Shelf.StorageRack.Row,
                StorageRackRowPosition = ps.Shelf.StorageRack.RowPosition,
                ShelfLevel = ps.Shelf.Level
            }).OrderBy(p => p.StorageName).ThenBy(p => p.StorageRackRow).ThenBy(p => p.StorageRackRowPosition).ToList();
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchString)
        {
            var searchResult = await _productRepository.SearchProductsAsync(searchString);
            return searchResult.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Width = product.Width,
                Length = product.Length,
                Height = product.Height,
                Weight = product.Weight,
                Description = product.Description
            }).ToList();
        }
    }
}
