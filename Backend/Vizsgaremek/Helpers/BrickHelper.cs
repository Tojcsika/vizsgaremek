using Vizsgaremek.Models.Bricks;
using Vizsgaremek.Repositories;

namespace Vizsgaremek.Helpers
{
    public static class BrickHelper
    {
        public static void Configure(IBrickRepository brickRepository)
        {
            var bricks = brickRepository.GetAll();
            Bricks.AddRange(bricks);
        }

        static BrickHelper() { }

        public static List<BrickType> Bricks { get; set; } = new List<BrickType>();

        public static List<BrickType> GetBricks()
        {
            return Bricks;
        }
    }
}
