using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;
using Vizsgaremek.Services;

namespace VizsgaremekUnitTests.Service
{
    [TestFixture]
    public class ShelfServiceTests
    {
        private ShelfRepository _shelfRepository;
        private ShelfService _shelfService;
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DatabaseContext(_options);
            _shelfRepository = new ShelfRepository(_context);
            _shelfService = new ShelfService(_shelfRepository);
        }

        [Test]
        public async Task GetShelfByIdAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = 1,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100,
                ShelfProducts = new List<ShelfProduct>()
            };
            _context.Shelves.Add(testItem);
            _context.SaveChanges();

            // Act
            var result = await _shelfService.GetShelfByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.StorageRackId, Is.EqualTo(1));
            Assert.That(result.Level, Is.EqualTo(1));
            Assert.That(result.Width, Is.EqualTo(1));
            Assert.That(result.Height, Is.EqualTo(1));
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.WeightLimit, Is.EqualTo(100));
            _context.Shelves.Remove(testItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllShelvesAsync()
        {
            // Arrange
            var testItems = new List<Shelf>
            {
                new Shelf { Id = 2, StorageRackId = 1, Level = 2, Width = 2, Height = 2, Length = 2, WeightLimit = 200 },
                new Shelf { Id = 3, StorageRackId = 1, Level = 3, Width = 3, Height = 3, Length = 3, WeightLimit = 300 },
                new Shelf { Id = 4, StorageRackId = 1, Level = 4, Width = 4, Height = 4, Length = 4, WeightLimit = 400 }
            };
            _context.Shelves.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _shelfService.GetAllShelvesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(testItems.Count));
            Assert.IsTrue(result.All(s => testItems.Any(t => t.Id == s.Id &&
                                                             t.StorageRackId == s.StorageRackId &&
                                                             t.Level == s.Level &&
                                                             t.Width == s.Width &&
                                                             t.Height == s.Height &&
                                                             t.Length == s.Length &&
                                                             t.WeightLimit == s.WeightLimit)));
            _context.Shelves.RemoveRange(testItems);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddShelfAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new ShelfDto()
            {
                Id = testItemId,
                StorageRackId = 1,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };
            _context.SaveChanges();

            // Act
            await _shelfService.CreateShelfAsync(testItem);

            // Assert
            var result = _context.Shelves.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.StorageRackId, Is.EqualTo(testItem.StorageRackId));
            Assert.That(result.Level, Is.EqualTo(testItem.Level));
            Assert.That(result.Width, Is.EqualTo(testItem.Width));
            Assert.That(result.Height, Is.EqualTo(testItem.Height));
            Assert.That(result.Length, Is.EqualTo(testItem.Length));
            Assert.That(result.WeightLimit, Is.EqualTo(testItem.WeightLimit));
            _context.Shelves.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateShelfAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = 1,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };
            _context.Shelves.Add(testItem);
            _context.SaveChanges();

            var updateShelf = new ShelfDto()
            {
                StorageRackId = 1,
                Level = 2,
                Width = 2,
                Height = 2,
                Length = 2,
                WeightLimit = 200
            };

            // Act
            await _shelfService.UpdateShelfAsync(testItemId, updateShelf);

            // Assert
            var result = _context.Shelves.Find(testItemId);
            Assert.IsNotNull(result);
            Assert.That(result.StorageRackId, Is.EqualTo(testItem.StorageRackId));
            Assert.That(result.Level, Is.EqualTo(testItem.Level));
            Assert.That(result.Width, Is.EqualTo(testItem.Width));
            Assert.That(result.Height, Is.EqualTo(testItem.Height));
            Assert.That(result.Length, Is.EqualTo(testItem.Length));
            Assert.That(result.WeightLimit, Is.EqualTo(testItem.WeightLimit));
            _context.Shelves.Remove(testItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteShelfAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = 1,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };
            _context.Shelves.Add(testItem);
            _context.SaveChanges();

            // Act
            await _shelfService.DeleteShelfAsync(testItemId);

            // Assert
            var result = _context.Shelves.Find(testItem.Id);
            Assert.IsNull(result);
            if (result != null)
            {
                _context.Shelves.Remove(result);
            }
            _context.SaveChanges();
        }
    }
}
