using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Identity;

namespace HatliFood.Models
{
    [Table("Restaurant")]
    public partial class Restaurant
    {
        [ForeignKey(nameof(User)), Key]
        [ValidateNever]
        public string Id { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string EmailAddress { get; set; }


        [DataType(DataType.Password)]
        [Required]

        public string Password { get; set; }


        [ValidateNever]
        public IdentityUser? User { get; set; }

        [Required]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string? Details { get; set; }

        public string? Location { get; set; }

        public string? City { get; set; }

        [Required]
        [ValidateNever]

        public string? ImgPath { get; set; }

        [NotMapped]
        [ValidateNever]

        public IFormFile ImgFile { get; set; }

        public virtual ICollection<Category> Categories { get; } = new List<Category>();

        public virtual ICollection<Order> ROrders { get; } = new List<Order>();
    }
}
