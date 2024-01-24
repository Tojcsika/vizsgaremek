namespace Vizsgaremek.Dtos
{
    public class ProductShelfDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double? ProductWeight { get; set; }
        public int ShelfId { get; set; }
        public int ShelfProductQuantity { get; set; }
        public string StorageName { get; set; }
        public int StorageRackRow { get; set; }
        public int StorageRackRowPosition { get; set; }
        public int ShelfLevel { get; set; }

    }
}