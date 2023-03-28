using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HatliFood.Models
{

    [Table("DeliveryGuy")]
    public partial class DeliveryGuy : IPerson
    {
        [Key]
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public int PhoneNumber { get; set; }

        [InverseProperty("DeliveryGuyUser")]
        public virtual ICollection<Order> Orders { get; } = new List<Order>();
    }

}
