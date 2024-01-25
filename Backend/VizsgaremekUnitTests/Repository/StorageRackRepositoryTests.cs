using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;

namespace VizsgaremekUnitTests.Repository
{
    [TestFixture]
    public class StorageRackRepositoryTests
    {
        private StorageRackRepository _storageRackRepository;
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DatabaseContext(_options);
            _storageRackRepository = new StorageRackRepository(_context);
        }

        [Test]
        public async Task GetStorageRackByIdAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testItemId = 1;
            var testItem = new StorageRack()
            {
                Id = testItemId,
                StorageId = testStorage.Id,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testItem);
            _context.SaveChanges();

            // Act
            var result = await _storageRackRepository.GetStorageRackByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.Row, Is.EqualTo(1));
            Assert.That(result.RowPosition, Is.EqualTo(1));
            Assert.That(result.WeightLimit, Is.EqualTo(100));
            Assert.That(result.StorageId, Is.EqualTo(testStorage.Id));
            _context.StorageRacks.Remove(testItem);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllStorageRacksAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testItems = new List<StorageRack>
            {
                new StorageRack { Id = 2, StorageId = 1, Row = 2, RowPosition = 2, WeightLimit = 200 },
                new StorageRack { Id = 3, StorageId = 1, Row = 3, RowPosition = 3, WeightLimit = 300 },
                new StorageRack { Id = 4, StorageId = 1, Row = 4, RowPosition = 4, WeightLimit = 400 }
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _storageRackRepository.GetAllStorageRacksAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(testItems.Count));
            Assert.IsTrue(result.All(s => testItems.Any(t => t.Id == s.Id && 
                                                             t.StorageId == s.StorageId && 
                                                             t.Row == s.Row && 
                                                             t.RowPosition == s.RowPosition && 
                                                             t.WeightLimit == s.WeightLimit)));
            _context.StorageRacks.RemoveRange(testItems);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddStorageRackAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var newStorageRack = new StorageRack()
            {
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            _context.Storages.Add(testStorage);
            _context.SaveChanges();

            // Act
            await _storageRackRepository.CreateStorageRackAsync(newStorageRack);

            // Assert
            var result = _context.StorageRacks.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.StorageId, Is.EqualTo(newStorageRack.StorageId));
            Assert.That(result.Row, Is.EqualTo(newStorageRack.Row));
            Assert.That(result.RowPosition, Is.EqualTo(newStorageRack.RowPosition));
            Assert.That(result.WeightLimit, Is.EqualTo(newStorageRack.WeightLimit));
            _context.StorageRacks.Remove(newStorageRack);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateStorageRackAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testItemId = 1;
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(new StorageRack()
            {
                Id = testItemId,
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            });
            _context.SaveChanges();
            var existingStorageRack = _context.StorageRacks.Find(testItemId);
            existingStorageRack.Row = 2;
            existingStorageRack.RowPosition = 2;
            existingStorageRack.WeightLimit = 200;

            // Act
            await _storageRackRepository.UpdateStorageRackAsync(existingStorageRack);

            // Assert
            var result = _context.StorageRacks.Find(testItemId);
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(existingStorageRack.Id));
            Assert.That(result.Row, Is.EqualTo(existingStorageRack.Row));
            Assert.That(result.RowPosition, Is.EqualTo(existingStorageRack.RowPosition));
            Assert.That(result.WeightLimit, Is.EqualTo(existingStorageRack.WeightLimit));
            _context.StorageRacks.Remove(result);
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteStorageRackAsync()
        {
            // Arrange
            var testStorage = new Storage()
            {
                Id = 1,
                Name = "Test Item",
                Address = "Test Address",
            };
            var testItem = new StorageRack()
            {
                StorageId = testStorage.Id,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            _context.Storages.Add(testStorage);
            _context.StorageRacks.Add(testItem);
            _context.SaveChanges();

            // Act
            await _storageRackRepository.DeleteStorageRackAsync(testItem);

            // Assert
            var result = _context.StorageRacks.Find(testItem.Id);
            Assert.IsNull(result);
            if (result != null)
            {
                _context.StorageRacks.Remove(result);
            }
            _context.Storages.Remove(testStorage);
            _context.SaveChanges();
        }
    }
}
