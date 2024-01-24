using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Entities
{
    public class Product : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<ShelfProduct> ShelfProducts { get; set; }
    }
}
