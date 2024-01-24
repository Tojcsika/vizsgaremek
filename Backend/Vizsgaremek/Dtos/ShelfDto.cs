using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Dtos
{
    public class ShelfDto
    {
        public int Id { get; set; }
        public int StorageRackId { get; set; }
        [Required(ErrorMessage = "Level is required.")]
        public int Level { get; set; }
        [Required(ErrorMessage = "Width is required.")]
        public double Width { get; set; }
        [Required(ErrorMessage = "Length is required.")]
        public double Length { get; set; }
        [Required(ErrorMessage = "Height is required.")]
        public double Height { get; set; }
        public double? WeightLimit { get; set; }
        public int TotalProducts { get; set; }
        public IEnumerable<ShelfProductDto> ShelfProducts { get; set; } = new List<ShelfProductDto>();
    }
}
