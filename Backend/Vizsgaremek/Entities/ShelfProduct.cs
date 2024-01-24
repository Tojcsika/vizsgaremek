using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Entities
{
    public class ShelfProduct : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int ShelfId { get; set; }
        public virtual Shelf Shelf { get; set; }
        public int Quantity { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
    }
}
