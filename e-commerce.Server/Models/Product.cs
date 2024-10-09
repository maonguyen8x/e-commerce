using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace e_commerce.Server.Models
{
    public partial class Product : BaseModel<long>
    {
        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }

        public string Name { get; set; }
        //public string? Image { get; set; }
        public string? Color { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quality { get; set; }
        //public int CategoryId { get; set; }
        //public virtual Category Category { get; set; } = null!;
    }
}
