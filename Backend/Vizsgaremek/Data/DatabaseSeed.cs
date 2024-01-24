using Vizsgaremek.Entities;
using Microsoft.EntityFrameworkCore;

namespace Vizsgaremek.Data
{
    public static class DatabaseSeed
    {
        static List<Shelf> shelves = new List<Shelf>();
        static List<Storage> storages = new List<Storage>();
        static List<Product> products = new List<Product>();
        static List<StorageRack> storageRacks = new List<StorageRack>();

        public static void Initialize(DatabaseContext dbContext)
        {
            dbContext.Database.Migrate();
            SeedStorages(dbContext);
            SeedProducts(dbContext);
            SeedStorageRacks(dbContext);
            SeedShelves(dbContext);
            SeedShelfProducts(dbContext);
        }

        private static void SeedStorages(DatabaseContext dbContext)
        {
            storages = new List<Storage>()
            {
                new Storage()
                {
                    Name = "Main Workshop",
                    Address = "North Pole, Arctic Circle",
                    Area = 200
                },
                new Storage()
                {
                    Name = "Wrapping Center",
                    Address = "North Pole, Arctic Circle",
                    Area = 150
                },
                new Storage()
                {
                    Name = "Gift Storage",
                    Address = "North Pole, Arctic Circle",
                    Area = 180
                }
            };

            if(!dbContext.Storages.Any())
            {
                dbContext.Storages.AddRange(storages);
                dbContext.SaveChanges();
            }
        }
        private static void SeedProducts(DatabaseContext dbContext)
        {
            products = new List<Product>()
            {
                new Product()
                {
                    Name = "Toy Train",
                    Width = 10,
                    Length = 20,
                    Height = 5,
                    Weight = 50,
                    Description = "Wooden train set"
                },
                new Product()
                {
                    Name = "Doll",
                    Width = 8,
                    Length = 15,
                    Height = 12,
                    Weight = 30,
                    Description = "Classic doll"
                },
                new Product()
                {
                    Name = "Teddy Bear",
                    Width = 12,
                    Length = 10,
                    Height = 8,
                    Weight = 20,
                    Description = "Soft and cuddly bear"
                },
                new Product()
                {
                    Name = "Remote Control Car",
                    Width = 15,
                    Length = 25,
                    Height = 10,
                    Weight = 80,
                    Description = "Racing car with remote control"
                },
                new Product()
                {
                    Name = "Puzzle",
                    Width = 5,
                    Length = 5,
                    Height = 2,
                    Weight = 10,
                    Description = "100-piece jigsaw puzzle"
                },
                new Product()
                {
                    Name = "Building Blocks",
                    Width = 10,
                    Length = 10,
                    Height = 10,
                    Weight = 40,
                    Description = "Colorful building blocks"
                },
                new Product()
                {
                    Name = "Bicycle",
                    Width = 20,
                    Length = 40,
                    Height = 10,
                    Weight = 120,
                    Description = "Kid's bicycle"
                },
                new Product()
                {
                    Name = "Board Game",
                    Width = 12,
                    Length = 12,
                    Height = 2,
                    Weight = 15,
                    Description = "Family board game"
                },
                new Product()
                {
                    Name = "Stuffed Penguin",
                    Width = 8,
                    Length = 8,
                    Height = 15,
                    Weight = 25,
                    Description = "Adorable stuffed penguin"
                },
                new Product()
                {
                    Name = "Play Kitchen Set",
                    Width = 18,
                    Length = 24,
                    Height = 15,
                    Weight = 60,
                    Description = "Mini kitchen playset"
                },
            };

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }

        private static void SeedStorageRacks(DatabaseContext dbContext)
        {
            storageRacks = new List<StorageRack>()
            {
                new StorageRack()
                {
                    StorageId = 1,
                    Row = 1,
                    RowPosition = 1,
                    WeightLimit = 1000
                },
                new StorageRack()
                {
                    StorageId = 1,
                    Row = 1,
                    RowPosition = 2,
                    WeightLimit = 1200
                },
                new StorageRack()
                {
                    StorageId = 1,
                    Row = 2,
                    RowPosition = 1,
                    WeightLimit = 800
                },
                new StorageRack()
                {
                    StorageId = 1,
                    Row = 2,
                    RowPosition = 2,
                    WeightLimit = 1000
                },
                new StorageRack()
                {
                    StorageId = 2,
                    Row = 1,
                    RowPosition = 1,
                    WeightLimit = 500
                },
                new StorageRack()
                {
                    StorageId = 2,
                    Row = 1,
                    RowPosition = 2,
                    WeightLimit = 700
                },
                new StorageRack()
                {
                    StorageId = 2,
                    Row = 2,
                    RowPosition = 1,
                    WeightLimit = 600
                },
                new StorageRack()
                {
                    StorageId = 3,
                    Row = 1,
                    RowPosition = 1,
                    WeightLimit = 800
                },
                new StorageRack()
                {
                    StorageId = 3,
                    Row = 1,
                    RowPosition = 2,
                    WeightLimit = 1000
                },
                new StorageRack()
                {
                    StorageId = 3,
                    Row = 2,
                    RowPosition = 1,
                    WeightLimit = 1200
                },
                new StorageRack()
                {
                    StorageId = 3,
                    Row = 2,
                    RowPosition = 2,
                    WeightLimit = 1500
                },
            };

            if (!dbContext.StorageRacks.Any())
            {
                dbContext.StorageRacks.AddRange(storageRacks);
                dbContext.SaveChanges();
            }
        }

        private static void SeedShelves(DatabaseContext dbContext)
        {
            shelves = new List<Shelf>()
            {
                new Shelf()
                {
                    StorageRackId = 1,
                    Level = 1,
                    Width = 50,
                    Length = 100,
                    Height = 30,
                    WeightLimit = 2000
                },
                new Shelf()
                {
                    StorageRackId = 1,
                    Level = 2,
                    Width = 40,
                    Length = 80,
                    Height = 25,
                    WeightLimit = 1500
                },
                new Shelf()
                {
                    StorageRackId = 2,
                    Level = 1,
                    Width = 30,
                    Length = 60,
                    Height = 20,
                    WeightLimit = 1000
                },
                new Shelf()
                {
                    StorageRackId = 2,
                    Level = 2,
                    Width = 35,
                    Length = 70,
                    Height = 22,
                    WeightLimit = 1200
                },
                new Shelf()
                {
                    StorageRackId = 3,
                    Level = 1,
                    Width = 20,
                    Length = 40,
                    Height = 15,
                    WeightLimit = 800
                },
                new Shelf()
                {
                    StorageRackId = 3,
                    Level = 2,
                    Width = 25,
                    Length = 50,
                    Height = 18,
                    WeightLimit = 1000
                },
                new Shelf()
                {
                    StorageRackId = 4,
                    Level = 1,
                    Width = 15,
                    Length = 30,
                    Height = 12,
                    WeightLimit = 700
                },
                new Shelf()
                {
                    StorageRackId = 4,
                    Level = 2,
                    Width = 18,
                    Length = 36,
                    Height = 15,
                    WeightLimit = 900
                },
                new Shelf()
                {
                    StorageRackId = 5,
                    Level = 1,
                    Width = 22,
                    Length = 44,
                    Height = 18,
                    WeightLimit = 1200
                },
                new Shelf()
                {
                    StorageRackId = 5,
                    Level = 2,
                    Width = 28,
                    Length = 56,
                    Height = 20,
                    WeightLimit = 1500
                },
                new Shelf()
                {
                    StorageRackId = 6,
                    Level = 1,
                    Width = 18,
                    Length = 36,
                    Height = 15,
                    WeightLimit = 900
                },
                new Shelf()
                {
                    StorageRackId = 6,
                    Level = 2,
                    Width = 20,
                    Length = 40,
                    Height = 17,
                    WeightLimit = 1100
                },
                new Shelf()
                {
                    StorageRackId = 7,
                    Level = 1,
                    Width = 25,
                    Length = 50,
                    Height = 22,
                    WeightLimit = 1400
                },
                new Shelf()
                {
                    StorageRackId = 7,
                    Level = 2,
                    Width = 50,
                    Length = 30,
                    Height = 60,
                    WeightLimit = 2500
                },
                new Shelf()
                {
                    StorageRackId = 8,
                    Level = 1,
                    Width = 40,
                    Length = 80,
                    Height = 30,
                    WeightLimit = 1800
                },
                new Shelf()
                {
                    StorageRackId = 8,
                    Level = 2,
                    Width = 45,
                    Length = 90,
                    Height = 35,
                    WeightLimit = 2000
                },
                new Shelf()
                {
                    StorageRackId = 9,
                    Level = 1,
                    Width = 50,
                    Length = 100,
                    Height = 40,
                    WeightLimit = 2200
                },
                new Shelf()
                {
                    StorageRackId = 9,
                    Level = 2,
                    Width = 55,
                    Length = 110,
                    Height = 45,
                    WeightLimit = 2400
                },
                new Shelf()
                {
                    StorageRackId = 10,
                    Level = 1,
                    Width = 60,
                    Length = 120,
                    Height = 50,
                    WeightLimit = 2600
                },
                new Shelf()
                {
                    StorageRackId = 10,
                    Level = 2,
                    Width = 65,
                    Length = 130,
                    Height = 55,
                    WeightLimit = 2800
                },
                new Shelf()
                {
                    StorageRackId = 11,
                    Level = 1,
                    Width = 70,
                    Length = 140,
                    Height = 60,
                    WeightLimit = 3000
                },
                new Shelf()
                {
                    StorageRackId = 11,
                    Level = 2,
                    Width = 75,
                    Length = 150,
                    Height = 65,
                    WeightLimit = 3200
                },
            };

            if (!dbContext.Shelves.Any())
            {
                dbContext.Shelves.AddRange(shelves);
                dbContext.SaveChanges();
            }
        }

        private static void SeedShelfProducts(DatabaseContext dbContext)
        {
            if (!dbContext.ShelfProducts.Any())
            {
                int productStorageNumber = 100;
                for (int i = 0; i < productStorageNumber; i++)
                {
                    var product = dbContext.Products.Skip(new Random().Next(dbContext.Products.Count())).First();
                    var shelfId = dbContext.Shelves.Skip(new Random().Next(dbContext.Shelves.Count())).First().Id;
                    var quantity = new Random().Next(1, 50);

                    dbContext.ShelfProducts.Add(new ShelfProduct
                    {
                        ProductId = product.Id,
                        ShelfId = shelfId,
                        Quantity = quantity,
                        Width = new Random().Next(1, 20) * 10,
                        Length = new Random().Next(1, 20) * 10,
                        Height = new Random().Next(1, 20) * 10
                    });
                }
                dbContext.SaveChanges();
            }
        }
    }
}
