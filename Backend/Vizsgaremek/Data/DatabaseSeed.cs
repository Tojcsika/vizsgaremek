using Vizsgaremek.Entities;
using Microsoft.EntityFrameworkCore;

namespace Vizsgaremek.Data
{
    public static class DatabaseSeed
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.Migrate();
            SeedBrickData(context);
        }

        private static void SeedBrickData(DatabaseContext context)
        {
            List<BrickEntity> bricks = new List<BrickEntity>()
            {
                new BrickEntity()
                {
                    DesignId = 3005,
                    Name = "BRICK 1x1",
                    Width = 1,
                    Height = 1,
                    Length = 1,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 8
                },
                new BrickEntity()
                {
                    DesignId = 3004,
                    Name = "BRICK 1x2",
                    Width = 1,
                    Height = 1,
                    Length = 2,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 16
                },
                new BrickEntity()
                {
                    DesignId = 3622,
                    Name = "BRICK 1x3",
                    Width = 1,
                    Height = 1,
                    Length = 3,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 24
                },
                new BrickEntity()
                {
                    DesignId = 3010,
                    Name = "BRICK 1x4",
                    Width = 1,
                    Height = 1,
                    Length = 4,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 32
                },
                new BrickEntity()
                {
                    DesignId = 3009,
                    Name = "BRICK 1x6",
                    Width = 1,
                    Height = 1,
                    Length = 6,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 48
                },
                new BrickEntity()
                {
                    DesignId = 3008,
                    Name = "BRICK 1x8",
                    Width = 1,
                    Height = 1,
                    Length = 8,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 64
                },
                new BrickEntity()
                {
                    DesignId = 6111,
                    Name = "BRICK 1x10",
                    Width = 1,
                    Height = 1,
                    Length = 10,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 80
                },
                new BrickEntity()
                {
                    DesignId = 6112,
                    Name = "BRICK 1x12",
                    Width = 1,
                    Height = 1,
                    Length = 12,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 96
                },
                new BrickEntity()
                {
                    DesignId = 2465,
                    Name = "BRICK 1x16",
                    Width = 1,
                    Height = 1,
                    Length = 16,
                    WidthSize = 8,
                    HeightSize = 9.6f,
                    LengthSize = 128
                },
                new BrickEntity()
                {
                    DesignId = 3004,
                    Name = "BRICK 2x1",
                    Width = 2,
                    Height = 1,
                    Length = 1,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 8
                },
                new BrickEntity()
                {
                    DesignId = 3003,
                    Name = "BRICK 2x2",
                    Width = 2,
                    Height = 1,
                    Length = 2,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 16
                },
                new BrickEntity()
                {
                    DesignId = 3002,
                    Name = "BRICK 2x3",
                    Width = 2,
                    Height = 1,
                    Length = 3,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 24
                },
                new BrickEntity()
                {
                    DesignId = 3001,
                    Name = "BRICK 2x4",
                    Width = 2,
                    Height = 1,
                    Length = 4,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 32
                },
                new BrickEntity()
                {
                    DesignId = 44237,
                    Name = "BRICK 2x6",
                    Width = 2,
                    Height = 1,
                    Length = 6,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 48
                },
                new BrickEntity()
                {
                    DesignId = 93888,
                    Name = "BRICK 2x8",
                    Width = 2,
                    Height = 1,
                    Length = 8,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 64
                },
                new BrickEntity()
                {
                    DesignId = 92538,
                    Name = "BRICK 2x10",
                    Width = 2,
                    Height = 1,
                    Length = 10,
                    WidthSize = 16,
                    HeightSize = 9.6f,
                    LengthSize = 80
                },
            };

            foreach (var brick in bricks)
            {
                if (context.Bricks.FirstOrDefault(b => b.Name == brick.Name) == null)
                {
                    context.Bricks.Add(brick);
                }
            }
            context.SaveChanges();
        }
    }
}
