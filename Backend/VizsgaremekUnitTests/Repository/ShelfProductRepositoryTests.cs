using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;

namespace VizsgaremekUnitTests.Repository
{
    [TestFixture]
    public class ShelfProductRepositoryTests
    {
        private ShelfProductRepository _shelfProductRepository;
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
        }

        [Test]
        public async Task GetShelfProductByIdAsync()
        {
            // Arrange
            var testItemId = 1;
            var testShelfProduct = new ShelfProduct()
            {
                Id = testItemId,
                ProductId = 1,
                ShelfId = 1,
                Quantity = 1,
                Width = 1,
                Length = 1,
                Height = 1
            };
            _context.ShelfProducts.Add(testShelfProduct);
            _context.SaveChanges();

            // Act
            var result = await _shelfProductRepository.GetShelfProductByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.ProductId, Is.EqualTo(1));
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
                new ShelfProduct { Id = 2, ProductId = 1, ShelfId = 1, Quantity = 1, Width = 1, Length = 1, Height = 1 },
                new ShelfProduct { Id = 3, ProductId = 2, ShelfId = 2, Quantity = 2, Width = 2, Length = 2, Height = 2 },
                new ShelfProduct { Id = 4, ProductId = 3, ShelfId = 3, Quantity = 3, Width = 3, Length = 3, Height = 3 }
            };
            _context.ShelfProducts.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _shelfProductRepository.GetAllShelfProductsAsync();

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
            var testShelfProduct = new ShelfProduct()
            {
                Id = 1,
                ProductId = 1,
                ShelfId = 1,
                Quantity = 1,
                Width = 1,
                Length = 1,
                Height = 1
            };
            _context.SaveChanges();

            // Act
            await _shelfProductRepository.CreateShelfProductAsync(testShelfProduct);

            // Assert
            var result = _context.ShelfProducts.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.ProductId, Is.EqualTo(testShelfProduct.ProductId));
            Assert.That(result.ShelfId, Is.EqualTo(testShelfProduct.ShelfId));
            Assert.That(result.Quantity, Is.EqualTo(testShelfProduct.Quantity));
            Assert.That(result.Width, Is.EqualTo(testShelfProduct.Width));
            Assert.That(result.Length, Is.EqualTo(testShelfProduct.Length));
            Assert.That(result.Height, Is.EqualTo(testShelfProduct.Height));
            _context.ShelfProducts.Remove(testShelfProduct);
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
            var existingShelfProduct = _context.ShelfProducts.Find(1);
            existingShelfProduct.ProductId = 2;
            existingShelfProduct.ShelfId = 2;
            existingShelfProduct.Quantity = 2;
            existingShelfProduct.Width = 2;
            existingShelfProduct.Length = 2;
            existingShelfProduct.Height = 2;

            // Act
            await _shelfProductRepository.UpdateShelfProductAsync(existingShelfProduct);

            // Assert
            var result = _context.ShelfProducts.Find(1);
            Assert.IsNotNull(result);
            Assert.That(result.ProductId, Is.EqualTo(existingShelfProduct.ProductId));
            Assert.That(result.ShelfId, Is.EqualTo(existingShelfProduct.ShelfId));
            Assert.That(result.Quantity, Is.EqualTo(existingShelfProduct.Quantity));
            Assert.That(result.Width, Is.EqualTo(existingShelfProduct.Width));
            Assert.That(result.Length, Is.EqualTo(existingShelfProduct.Length));
            Assert.That(result.Height, Is.EqualTo(existingShelfProduct.Height));
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
            await _shelfProductRepository.DeleteShelfProductAsync(testItem);
             
            // Assert
            var result = _context.ShelfProducts.Find(testItem.Id);
            Assert.IsNull(result);
            if (result != null)       
            {
                _context.ShelfProducts.Remove(result);
            }
            _context.SaveChanges();
        }

        [Test]
        public async Task GetShelfProductsByProductId()
        {
            // Arrange
            var testItems = new List<ShelfProduct>
            {
                new ShelfProduct { Id = 2, ProductId = 1, ShelfId = 1, Quantity = 1, Width = 1, Length = 1, Height = 1 },
                new ShelfProduct { Id = 3, ProductId = 2, ShelfId = 2, Quantity = 2, Width = 2, Length = 2, Height = 2 },
                new ShelfProduct { Id = 4, ProductId = 3, ShelfId = 3, Quantity = 3, Width = 3, Length = 3, Height = 3 },
                new ShelfProduct { Id = 5, ProductId = 3, ShelfId = 4, Quantity = 4, Width = 4, Length = 4, Height = 4 }
            };
            _context.ShelfProducts.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _shelfProductRepository.GetShelfProductsByProductId(3);
             
            // Assert
            Assert.NotNull(result);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.Any(r => r.Id == 4), Is.EqualTo(true));
            Assert.That(result.Any(r => r.Id == 5), Is.EqualTo(true));

            _context.ShelfProducts.RemoveRange(testItems);
            _context.SaveChanges();
        }
    }
}
