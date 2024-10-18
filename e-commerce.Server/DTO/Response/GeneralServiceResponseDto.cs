namespace e_commerce.Server.DTO.Response
{
    public class GeneralServiceResponseDto
    {
        public bool IsSucceed { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public object? Data { get; set; }
    }
}
