using Microsoft.EntityFrameworkCore;
using Vizsgaremek.Data;
using Vizsgaremek.Entities;
using Vizsgaremek.Repositories;

namespace VizsgaremekUnitTests.Repository
{
    [TestFixture]
    public class StorageRepositoryTests
    {
        private StorageRepository _storageRepository;
        private DbContextOptions<DatabaseContext> _options;
        private DatabaseContext _context;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new DatabaseContext(_options);
            _storageRepository = new StorageRepository(_context);
        }

        [Test]
        public async Task GetStorageByIdAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Storage()
            {
                Id = testItemId,
                Name = "Test Item",
                Address = "Test Address",
            };
            _context.Storages.Add(testItem);
            _context.SaveChanges();

            // Act
            var result = await _storageRepository.GetStorageByIdAsync(testItemId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(testItemId));
            Assert.That(result.Name, Is.EqualTo("Test Item"));
            Assert.That(result.Address, Is.EqualTo("Test Address"));
            _context.Storages.Remove(testItem);
            _context.SaveChanges();
        }

        [Test]
        public async Task GetAllStoragesAsync()
        {
            // Arrange
            var testItems = new List<Storage>
            {
                new Storage { Id = 2, Name = "Storage 2", Address = "Test Address 2" },
                new Storage { Id = 3, Name = "Storage 3", Address = "Test Address 3" },
                new Storage { Id = 4, Name = "Storage 4", Address = "Test Address 4" }
            };
            _context.Storages.AddRange(testItems);
            _context.SaveChanges();

            // Act
            var result = await _storageRepository.GetAllStoragesAsync();

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result.Count(), Is.EqualTo(testItems.Count));
            Assert.IsTrue(result.All(s => testItems.Any(t => t.Id == s.Id && t.Name == s.Name && t.Address == s.Address)));
            _context.Storages.RemoveRange(testItems);
            _context.SaveChanges();
        }

        [Test]
        public async Task AddStorageAsync()
        {
            // Arrange
            var newStorage = new Storage { Name = "New Storage", Address = "Test Address" };

            // Act
            await _storageRepository.CreateStorageAsync(newStorage);

            // Assert
            var result = _context.Storages.FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.That(result.Name, Is.EqualTo(newStorage.Name));
            Assert.That(result.Address, Is.EqualTo(newStorage.Address));
            _context.Storages.Remove(newStorage);
            _context.SaveChanges();
        }

        [Test]
        public async Task UpdateStorageAsync()
        {
            // Arrange
            var testItemId = 1;
            _context.Storages.Add(new Storage { Id = testItemId, Name = "Test Storage", Address = "Test Address" });
            _context.SaveChanges();
            var existingStorage = _context.Storages.Find(testItemId);
            existingStorage.Name = "Updated Storage";
            existingStorage.Address = "Updated Address";

            // Act
            await _storageRepository.UpdateStorageAsync(existingStorage);

            // Assert
            var result = _context.Storages.Find(testItemId);
            Assert.IsNotNull(result);
            Assert.That(result.Id, Is.EqualTo(existingStorage.Id));
            Assert.That(result.Name, Is.EqualTo(existingStorage.Name));
            _context.Storages.Remove(result);
            _context.SaveChanges();
        }

        [Test]
        public async Task DeleteStorageAsync()
        {
            // Arrange
            var testItemId = 1;
            var testItem = new Storage { Id = testItemId, Name = "Test Storage", Address = "Test Address" };
            _context.Storages.Add(testItem);
            _context.SaveChanges();

            // Act
            await _storageRepository.DeleteStorageAsync(testItem);

            // Assert
            var result = _context.Storages.Find(testItemId);
            Assert.IsNull(result);
        }
    }
}
