using e_commerce.Server.Models;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Server.DTO.Product
{
    public class AddProductDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Color { get; set; }
        public int Quality { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

        //public int CategoryId { get; set; }
        //public string? CategoryName { get; set; }
    }
}
