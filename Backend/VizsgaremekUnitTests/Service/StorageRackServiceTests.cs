using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Dtos;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;
using Vizsgaremek.Services;

namespace VizsgaremekUnitTests.Service
{
    [TestFixture]
    public class StorageRackServiceTests
    {
        private StorageRackRepository _storageRackRepository;
        private StorageRackService _storageRackService;
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
            _storageRackService = new StorageRackService(_storageRackRepository);
        }

        [Test]
        public async Task GetStorageRackByIdAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new StorageRack()
            {
                Id = testItemId,
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
                Shelves = new List<Shelf>()
            };
            _context.StorageRacks.Add(testItem);
            _context.SaveChanges();

            // Act
            var result = await _storageRackService.GetStorageRackByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.Row, Is.EqualTo(1));
            Assert.That(result.RowPosition, Is.EqualTo(1));
            Assert.That(result.WeightLimit, Is.EqualTo(100));
            Assert.That(result.StorageId, Is.EqualTo(1));
            _context.StorageRacks.Remove(testItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllStorageRacksAsync()
        {
            // Arrange
            var testItems = new List<StorageRack>
            {
                new StorageRack { Id = 2, StorageId = 1, Row = 2, RowPosition = 2, WeightLimit = 200, Shelves = new List<Shelf>() },
                new StorageRack { Id = 3, StorageId = 1, Row = 3, RowPosition = 3, WeightLimit = 300, Shelves = new List<Shelf>() },
                new StorageRack { Id = 4, StorageId = 1, Row = 4, RowPosition = 4, WeightLimit = 400, Shelves = new List<Shelf>() }
            };
            _context.StorageRacks.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _storageRackService.GetAllStorageRacksAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(testItems.Count));
            Assert.IsTrue(result.All(s => testItems.Any(t => t.Id == s.Id &&
                                                             t.StorageId == s.StorageId &&
                                                             t.Row == s.Row &&
                                                             t.RowPosition == s.RowPosition &&
                                                             t.WeightLimit == s.WeightLimit)));
            _context.StorageRacks.RemoveRange(testItems);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddStorageRackAsync()
        {
            // Arrange
            var newStorageRack = new StorageRackDto()
            {
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };

            // Act
            await _storageRackService.CreateStorageRackAsync(newStorageRack);

            // Assert
            var result = _context.StorageRacks.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.StorageId, Is.EqualTo(newStorageRack.StorageId));
            Assert.That(result.Row, Is.EqualTo(newStorageRack.Row));
            Assert.That(result.RowPosition, Is.EqualTo(newStorageRack.RowPosition));
            Assert.That(result.WeightLimit, Is.EqualTo(newStorageRack.WeightLimit));
            _context.StorageRacks.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateStorageRackAsync()
        {
            var testItemId = 1;
            var newStorageRack =  new StorageRack()
            {
                Id = testItemId,
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            _context.StorageRacks.Add(newStorageRack);
            _context.SaveChanges();
            var updateStorageRack = new StorageRackDto()
            {
                StorageId = 1,
                Row = 2,
                RowPosition = 2,
                WeightLimit = 200,
            };

            // Act
            await _storageRackService.UpdateStorageRackAsync(testItemId, updateStorageRack);

            // Assert
            var result = _context.StorageRacks.Find(testItemId);
            Assert.IsNotNull(result);
            Assert.That(result.Row, Is.EqualTo(updateStorageRack.Row));
            Assert.That(result.RowPosition, Is.EqualTo(updateStorageRack.RowPosition));
            Assert.That(result.WeightLimit, Is.EqualTo(updateStorageRack.WeightLimit));
            _context.StorageRacks.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteStorageRackAsync()
        {
            // Arrange
            var testItem = new StorageRack()
            {
                Id = 1,
                StorageId = 1,
                Row = 1,
                RowPosition = 1,
                WeightLimit = 100,
            };
            _context.StorageRacks.Add(testItem);
            _context.SaveChanges();

            // Act
            await _storageRackService.DeleteStorageRackAsync(testItem.Id);

            // Assert
            var result = _context.StorageRacks.Find(testItem.Id);
            Assert.IsNull(result);
            if (result != null)
            {
                _context.StorageRacks.Remove(result);
            }
            _context.SaveChanges();
        }
    }
}
