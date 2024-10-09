using System.ComponentModel.DataAnnotations;

namespace e_commerce.Server.DTO.Accounts
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
