
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace e_commerce.Server.Models 
{ 
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToUniversalTime();

        [NotMapped]
        public IList<string> Roles { get; set; }

    }
}