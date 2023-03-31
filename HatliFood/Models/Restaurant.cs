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
        public string ImgPath { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }

<<<<<<< HEAD
=======

        [InverseProperty("RidNavigation")]
>>>>>>> 2049b21f61e567cd4b46e9c8f44e374ab0b42a55
        public virtual ICollection<Category> Categories { get; } = new List<Category>();

        public virtual ICollection<Order> Orders { get; } = new List<Order>();
    }
}
