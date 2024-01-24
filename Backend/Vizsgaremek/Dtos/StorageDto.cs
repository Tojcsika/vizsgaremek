using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Dtos
{
    public class StorageDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Address { get; set; }
        public double? Area { get; set; }
        public int TotalStorageRacks { get; set; }
        public int TotalProducts { get; set; }
    }
}
