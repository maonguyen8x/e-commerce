using e_commerce.Server.Models;

namespace e_commerce.Server.DTO.Auth
{
    public class AccountRespDTO : BaseModel<long>
    {
        public string Token { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Initials { get; set; }
    }
}
