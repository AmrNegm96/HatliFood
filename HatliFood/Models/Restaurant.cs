using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HatliFood.Models
{
    [Table("Restaurant")]
    public partial class Restaurant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Details { get; set; }

        public string? Location { get; set; }

        public string? City { get; set; }

        [Required]
        public string? ImgPath { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }

        public virtual ICollection<Category> Categories { get; } = new List<Category>();

        public virtual ICollection<Order> ROrders { get; } = new List<Order>();
    }
}
