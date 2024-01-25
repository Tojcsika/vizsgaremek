using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;
using Vizsgaremek.Services;

namespace VizsgaremekUnitTests.Service
{
    public class ProductServiceTests
    {
        private ProductRepository _productRepository;
        private ShelfProductRepository _shelfProductRepository;
        private ProductService _productService;
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DatabaseContext(_options);
            _productRepository = new ProductRepository(_context);
            _shelfProductRepository = new ShelfProductRepository(_context);
            _productService = new ProductService(_productRepository, _shelfProductRepository);
        }

        [Test]
        public async Task GetProductByIdAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Product()
            {
                Id = testItemId,
                Name = "Test Item",
                Description = "Test Description",
                Width = 1,
                Height = 1,
                Length = 1,
                Weight = 10,
            };
            _context.Products.Add(testItem);
            _context.SaveChanges();

            // Act
            var result = await _productService.GetProductByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.Name, Is.EqualTo("Test Item"));
            Assert.That(result.Width, Is.EqualTo(1));
            Assert.That(result.Height, Is.EqualTo(1));
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.Weight, Is.EqualTo(10));
            _context.Products.Remove(testItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllProductsAsync()
        {
            // Arrange
            var testItems = new List<Product>
            {
                new Product { Id = 2, Name = "Product 2", Description = "Test Description 2", Width = 2, Height = 2, Length = 2, Weight = 20 },
                new Product { Id = 3, Name = "Product 3", Description = "Test Description 3", Width = 3, Height = 3, Length = 3, Weight = 30 },
                new Product { Id = 4, Name = "Product 4", Description = "Test Description 4", Width = 4, Height = 4, Length = 4, Weight = 40 }
            };
            _context.Products.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(testItems.Count));
            Assert.IsTrue(result.All(s => testItems.Any(t => t.Id == s.Id && 
                                                             t.Name == s.Name &&
                                                             t.Description == s.Description && 
                                                             t.Width == s.Width && 
                                                             t.Height == s.Height && 
                                                             t.Length == s.Length && 
                                                             t.Weight == s.Weight)));
            _context.Products.RemoveRange(testItems);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddProductAsync()
        {
            // Arrange
            var newProduct = new ProductDto { Name = "New Product", Description = "Test Description", Width = 1, Height = 1, Length = 1, Weight = 10 };

            // Act
            await _productService.CreateProductAsync(newProduct);

            // Assert
            var result = _context.Products.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(newProduct.Name));
            Assert.That(result.Description, Is.EqualTo(newProduct.Description));
            Assert.That(result.Width, Is.EqualTo(newProduct.Width));
            Assert.That(result.Height, Is.EqualTo(newProduct.Height));
            Assert.That(result.Length, Is.EqualTo(newProduct.Length));
            Assert.That(result.Weight, Is.EqualTo(newProduct.Weight));
            _context.Products.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateProductAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Product()
            {
                Id = testItemId,
                Name = "Test Item",
                Description = "Test Description",
                Width = 1,
                Height = 1,
                Length = 1,
                Weight = 10,
            };
            _context.Products.Add(testItem);
            _context.SaveChanges();

            var updateProduct = new ProductDto()
            {
                Id = testItemId,
                Name = "Updated Item",
                Description = "Updated Description",
                Width = 2,
                Height = 2,
                Length = 2,
                Weight = 20,
            };

            // Act
            await _productService.UpdateProductAsync(testItemId, updateProduct);

            // Assert
            var result = _context.Products.Find(testItemId);
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(updateProduct.Id));
            Assert.That(result.Name, Is.EqualTo(updateProduct.Name));
            Assert.That(result.Description, Is.EqualTo(updateProduct.Description));
            Assert.That(result.Width, Is.EqualTo(updateProduct.Width));
            Assert.That(result.Height, Is.EqualTo(updateProduct.Height));
            Assert.That(result.Length, Is.EqualTo(updateProduct.Length));
            Assert.That(result.Weight, Is.EqualTo(updateProduct.Weight));
            _context.Products.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteProductAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Product { Id = testItemId, Name = "New Product", Description = "Test Description", Width = 1, Height = 1, Length = 1, Weight = 10 };
            _context.Products.Add(testItem);
            _context.SaveChanges();

            // Act
            await _productService.DeleteProductAsync(testItemId);

            // Assert
            var result = _context.Products.Find(testItemId);
            Assert.IsNull(result);
            if (result != null)
            {
                _context.Products.Remove(result);
            }
            _context.SaveChanges();
        }

        [Test]
        public async Task SearchProductsAsync()
        {
            // Arrange
            var testItems = new List<Product>
            {
                new Product { Id = 2, Name = "Product 2", Description = "Test Description 2", Width = 2, Height = 2, Length = 2, Weight = 20 },
                new Product { Id = 3, Name = "Product 3", Description = "Test Description 3", Width = 3, Height = 3, Length = 3, Weight = 30 },
                new Product { Id = 4, Name = "Product 4", Description = "Test Description 4", Width = 4, Height = 4, Length = 4, Weight = 40 },
                new Product { Id = 5, Name = "Product 45", Description = "Test Description 5", Width = 5, Height = 5, Length = 5, Weight = 50 },
                new Product { Id = 6, Name = "Product 46", Description = "Test Description 6", Width = 6, Height = 6, Length = 4, Weight = 40 }
            };
            _context.Products.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _productService.SearchProductsAsync("Product 4");

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(3));
            Assert.That(result.Any(r => r.Id == 4), Is.EqualTo(true));
            Assert.That(result.Any(r => r.Id == 5), Is.EqualTo(true));
            Assert.That(result.Any(r => r.Id == 6), Is.EqualTo(true));
            _context.Products.RemoveRange(testItems);
            _context.SaveChanges();
        }
    }
}
