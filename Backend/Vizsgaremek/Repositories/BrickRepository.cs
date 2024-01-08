using Vizsgaremek.Data;
using Vizsgaremek.Models.Bricks;

namespace Vizsgaremek.Repositories
{
    public class BrickRepository : IBrickRepository
    {
        private DatabaseContext _databaseContext;
        public BrickRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<BrickType> GetAll()
        {
            var bricks = _databaseContext.Bricks.Select(b => new BrickType()
            {
                DesignId = b.DesignId,
                Name = b.Name,
                Width = b.Width,
                Height = b.Height,
                Length = b.Length,
                WidthSize = b.WidthSize,
                HeightSize = b.HeightSize,
                LengthSize = b.LengthSize
            }).ToList();
            return bricks;
        }
    }
}
