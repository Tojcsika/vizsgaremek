using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Dtos
{
    public class StorageRackDto
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        [Required(ErrorMessage = "Row is required.")]
        public int Row { get; set; }
        [Required(ErrorMessage = "RowPosition is required.")]
        public int RowPosition { get; set; }
        public int? WeightLimit { get; set; }
        public int Shelves { get; set; }
    }
}
