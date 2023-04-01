using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HatliFood.Models
{
    [Table("Restaurant")]
    public partial class Restaurant
    {
        [ForeignKey(nameof(User)), Key]
        public string Id { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? Details { get; set; }

        public string? Location { get; set; }

        public string? City { get; set; }

        [Required]
        public string ImgPath { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; } 

        public virtual ICollection<Category> Categories { get; } = new List<Category>();

        public virtual ICollection<Order> ROrders { get; } = new List<Order>();
    }
}
