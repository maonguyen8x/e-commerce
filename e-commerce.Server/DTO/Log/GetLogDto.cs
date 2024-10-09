namespace e_commerce.Server.DTO.Log
{
    public class GetLogDto
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
        public string Description  { get; set; }
    }
}
