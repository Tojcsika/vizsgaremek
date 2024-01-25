using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;

namespace VizsgaremekUnitTests.Repository
{
    [TestFixture]
    public class ShelfRepositoryTests
    {
        private ShelfRepository _shelfRepository;
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
        }

        [Test]
        public async Task GetShelfByIdAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testStorageRack = new StorageRack()
            {
                Id = 1,
                StorageId = testStorage.Id,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = testStorageRack.Id,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testStorageRack);
            _context.Shelves.Add(testItem);
            _context.SaveChanges();

            // Act
            var result = await _shelfRepository.GetShelfByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.StorageRackId, Is.EqualTo(testStorageRack.Id));
            Assert.That(result.Level, Is.EqualTo(1));
            Assert.That(result.Width, Is.EqualTo(1));
            Assert.That(result.Height, Is.EqualTo(1));
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.That(result.WeightLimit, Is.EqualTo(100));
            _context.Shelves.Remove(testItem);
            _context.StorageRacks.Remove(testStorageRack);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllShelvesAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testStorageRack = new StorageRack()
            {
                Id = 1,
                StorageId = testStorage.Id,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            var testItems = new List<Shelf>
            {
                new Shelf { Id = 2, StorageRackId = 1, Level = 2, Width = 2, Height = 2, Length = 2, WeightLimit = 200 },
                new Shelf { Id = 3, StorageRackId = 1, Level = 3, Width = 3, Height = 3, Length = 3, WeightLimit = 300 },
                new Shelf { Id = 4, StorageRackId = 1, Level = 4, Width = 4, Height = 4, Length = 4, WeightLimit = 400 }
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testStorageRack);
            _context.Shelves.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _shelfRepository.GetAllShelvesAsync();

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
            _context.StorageRacks.Remove(testStorageRack);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddShelfAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testStorageRack = new StorageRack()
            {
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = testStorageRack.Id,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testStorageRack);
            _context.SaveChanges();

            // Act
            await _shelfRepository.CreateShelfAsync(testItem);

            // Assert
            var result = _context.Shelves.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.StorageRackId, Is.EqualTo(testItem.StorageRackId));
            Assert.That(result.Level, Is.EqualTo(testItem.Level));
            Assert.That(result.Width, Is.EqualTo(testItem.Width));
            Assert.That(result.Height, Is.EqualTo(testItem.Height));
            Assert.That(result.Length, Is.EqualTo(testItem.Length));
            Assert.That(result.WeightLimit, Is.EqualTo(testItem.WeightLimit));
            _context.Shelves.Remove(testItem);
            _context.StorageRacks.Remove(testStorageRack);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateShelfAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testStorageRack = new StorageRack()
            {
                Id = 1,
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = testStorageRack.Id,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testStorageRack);
            _context.Shelves.Add(testItem);
            _context.SaveChanges();

            var existingShelf = _context.Shelves.Find(testItemId);
            existingShelf.Level = 2;
            existingShelf.Width = 2;
            existingShelf.Height = 2;
            existingShelf.Length = 2;
            existingShelf.WeightLimit = 200;

            // Act
            await _shelfRepository.UpdateShelfAsync(existingShelf);

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
            _context.StorageRacks.Remove(testStorageRack);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteShelfAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testStorageRack = new StorageRack()
            {
                StorageId = testStorage.Id,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            var testItemId = 1;
            var testItem = new Shelf()
            {
                Id = testItemId,
                StorageRackId = testStorageRack.Id,
                Level = 1,
                Width = 1,
                Height = 1,
                Length = 1,
                WeightLimit = 100
            };

            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testStorageRack);
            _context.Shelves.Add(testItem);
            _context.SaveChanges();

            // Act
            await _shelfRepository.DeleteShelfAsync(testItem);

            // Assert
            var result = _context.Shelves.Find(testItem.Id);
            Assert.IsNull(result);
            if (result != null)
            {
                _context.Shelves.Remove(result);
            }
            _context.StorageRacks.Remove(testStorageRack);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }
    }
}
