namespace e_commerce.Server.DTO.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UserName { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? Quality { get; set; }
        public string? Color { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
