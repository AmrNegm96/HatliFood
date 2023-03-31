using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HatliFood.Models
{

    [Table("DeliveryGuy")]
    public partial class DeliveryGuy : IPerson
    {
        [ForeignKey(nameof(User)), Key]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        public string Name { get; set; }

        public int PhoneNumber { get; set; }

        public virtual ICollection<Order> Orders { get; } = new List<Order>();
    }

}
