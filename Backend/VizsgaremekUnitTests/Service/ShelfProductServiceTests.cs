using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;
using Vizsgaremek.Services;

namespace VizsgaremekUnitTests.Service
{
    [TestFixture]
    public class ShelfProductServiceTests
    {
        private ShelfProductRepository _shelfProductRepository;
        private ShelfProductService _shelfProductService;
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DatabaseContext(_options);
            _shelfProductRepository = new ShelfProductRepository(_context);
            _shelfProductService = new ShelfProductService(_shelfProductRepository);
        }

        [Test]
        public async Task GetShelfProductByIdAsync()
        {
            // Arrange
            var testItemId = 1;
            var testShelfProduct = new ShelfProduct()
            {
                Id = testItemId,
                ProductId = 4,
                ShelfId = 1,
                Quantity = 1,
                Width = 1,
                Length = 1,
                Height = 1,
                Product = new Product { Id = 4, Name = "Product 1" }
            };
            _context.ShelfProducts.Add(testShelfProduct);
            _context.SaveChanges();

            // Act
            var result = await _shelfProductService.GetShelfProductByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.ProductId, Is.EqualTo(4));
            Assert.That(result.ShelfId, Is.EqualTo(1));
            Assert.That(result.Quantity, Is.EqualTo(1));
            Assert.That(result.Width, Is.EqualTo(1));
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.Height, Is.EqualTo(1));
            _context.ShelfProducts.Remove(testShelfProduct);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllShelfProductsAsync()
        {
            // Arrange
            var testItems = new List<ShelfProduct>
            {
                new ShelfProduct { Id = 2, ProductId = 1, ShelfId = 1, Quantity = 1, Width = 1, Length = 1, Height = 1, Product = new Product { Name = "Product 1" } },
                new ShelfProduct { Id = 3, ProductId = 2, ShelfId = 2, Quantity = 2, Width = 2, Length = 2, Height = 2, Product = new Product { Name = "Product 2" } },
                new ShelfProduct { Id = 4, ProductId = 3, ShelfId = 3, Quantity = 3, Width = 3, Length = 3, Height = 3, Product = new Product { Name = "Product 3" } }
            };
            _context.ShelfProducts.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _shelfProductService.GetAllShelfProductsAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(testItems.Count));
            Assert.IsTrue(result.All(s => testItems.Any(t => t.Id == s.Id &&
                                                             t.ProductId == s.ProductId &&
                                                             t.ShelfId == s.ShelfId &&
                                                             t.Quantity == s.Quantity &&
                                                             t.Width == s.Width &&
                                                             t.Length == s.Length &&
                                                             t.Height == s.Height)));
            _context.ShelfProducts.RemoveRange(testItems);
            _context.SaveChanges();
        }

        [Test]
        public async Task CreateShelfProductAsync()
        {
            // Arrange
            var testShelfProduct = new ShelfProductDto()
            {
                Id = 1,
                ProductId = 1,
                ShelfId = 1,
                Quantity = 1,
                Width = 1,
                Length = 1,
                Height = 1
            };

            // Act
            await _shelfProductService.CreateShelfProductAsync(testShelfProduct);

            // Assert
            var result = _context.ShelfProducts.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.ProductId, Is.EqualTo(testShelfProduct.ProductId));
            Assert.That(result.ShelfId, Is.EqualTo(testShelfProduct.ShelfId));
            Assert.That(result.Quantity, Is.EqualTo(testShelfProduct.Quantity));
            Assert.That(result.Width, Is.EqualTo(testShelfProduct.Width));
            Assert.That(result.Length, Is.EqualTo(testShelfProduct.Length));
            Assert.That(result.Height, Is.EqualTo(testShelfProduct.Height));
            _context.ShelfProducts.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateShelfProductAsync()
        {
            // Arrange
            _context.ShelfProducts.Add(new ShelfProduct()
            {
                Id = 1,
                ProductId = 1,
                ShelfId = 1,
                Quantity = 1,
                Width = 1,
                Length = 1,
                Height = 1
            });
            _context.SaveChanges();

            var updateShelfProduct = new ShelfProductDto()
            {
                Quantity = 2,
                Width = 2,
                Length = 2,
                Height = 2
            };

            // Act
            await _shelfProductService.UpdateShelfProductAsync(1, updateShelfProduct);

            // Assert
            var result = _context.ShelfProducts.Find(1);
            Assert.IsNotNull(result);
            Assert.That(result.Quantity, Is.EqualTo(updateShelfProduct.Quantity));
            Assert.That(result.Width, Is.EqualTo(updateShelfProduct.Width));
            Assert.That(result.Length, Is.EqualTo(updateShelfProduct.Length));
            Assert.That(result.Height, Is.EqualTo(updateShelfProduct.Height));
            _context.ShelfProducts.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteShelfProductAsync()
        {
            // Arrange
            var testItem = new ShelfProduct()
            {
                Id = 1,
                ProductId = 1,
                ShelfId = 1,
                Quantity = 1,
                Width = 1,
                Length = 1,
                Height = 1
            };
            _context.ShelfProducts.Add(testItem);
            _context.SaveChanges();

            // Act
            await _shelfProductService.DeleteShelfProductAsync(1);
             
            // Assert
            var result = _context.ShelfProducts.Find(testItem.Id);
            Assert.IsNull(result);
            if (result != null)       
            {
                _context.ShelfProducts.Remove(result);
            }
            _context.SaveChanges();
        }
    }
}
