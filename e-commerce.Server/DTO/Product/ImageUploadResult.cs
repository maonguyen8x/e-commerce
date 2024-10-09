namespace e_commerce.Server.DTO.Product
{
    public class ImageUploadResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ImageUrl { get; set; } // Optional for successful upload
    }
}
