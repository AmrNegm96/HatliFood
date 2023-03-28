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

        [Required]
        public string ImgPath { get; set; }

        [InverseProperty("RidNavigation")]
        public virtual ICollection<Category> Categories { get; } = new List<Category>();

        [InverseProperty("Restaurant")]
        public virtual ICollection<Order> Orders { get; } = new List<Order>();
    }
}
