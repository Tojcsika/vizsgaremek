using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Entities
{
    public class StorageRack : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int StorageId { get; set; }
        public virtual Storage Storage { get; set; }
        public int Row { get; set; }
        public int RowPosition { get; set; }
        public int? WeightLimit { get; set; }
        public virtual ICollection<Shelf> Shelves { get; set; }
    }
}
