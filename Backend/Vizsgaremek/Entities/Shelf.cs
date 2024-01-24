using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Entities
{
    public class Shelf : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int StorageRackId { get; set; }
        public virtual StorageRack StorageRack { get; set; }
        public int Level { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }
        public double Height { get; set; }
        public double? WeightLimit { get; set; }
        public virtual ICollection<ShelfProduct> ShelfProducts { get; set; }
    }
}
