namespace Vizsgaremek.Dtos
{
    public class ShelfProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public int ProductId { get; set; }
        public int ShelfId { get; set; }
        public int Quantity { get; set; }
        public double TotalWeight { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
    }
}
