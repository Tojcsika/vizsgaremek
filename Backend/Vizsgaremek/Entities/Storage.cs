using System.ComponentModel.DataAnnotations;

namespace Vizsgaremek.Entities
{
    public class Storage : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double? Area { get; set; }
        public virtual ICollection<StorageRack> StorageRacks { get; set; }
    }
}
