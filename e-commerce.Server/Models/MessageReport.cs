namespace e_commerce.Server.Models
{
    public class MessageReport
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public MessageReport()
        {
        }
        public MessageReport(bool IsSuccess, string Message)
        {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
        }
    }
}
