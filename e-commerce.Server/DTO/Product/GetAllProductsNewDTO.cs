namespace e_commerce.Server.DTO.Product
{
    public class GetAllProductsNewDTO
    {
        public List<string> AvailableColors { get; set; }
        public string Brand { get; set; }
        public long DateAdded { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<ImageCollectionItem> ImageCollection { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsRecommended { get; set; }
        public List<string> Keywords { get; set; }
        public int MaxQuantity { get; set; }
        public string Name { get; set; }
        public string NameLower { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<string> Sizes { get; set; }
    }

    public class ImageCollectionItem
    {
        public string Id { get; set; }
        public string Url { get; set; }
    }
}