using System.ComponentModel.DataAnnotations;

namespace e_commerce.Server.DTO.Accounts
{
    public class RegisterDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "UserName is required")]
        public string UserName { get; set; }
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} characters long", MinimumLength = 8)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
