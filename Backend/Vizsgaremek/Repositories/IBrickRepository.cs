using Vizsgaremek.Models.Bricks;

namespace Vizsgaremek.Repositories
{
    public interface IBrickRepository
    {
        List<BrickType> GetAll();
    }
}
