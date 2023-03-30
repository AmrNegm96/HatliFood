using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HatliFood.Models
{
    [Table("Order")]
    public partial class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderState { get; set; }


        [Column("BID")]
        public string BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        [InverseProperty("Orders")]
        public virtual Buyer Buyer { get; set; }

        [Column("deliveryGuyUserId")]
        public string DeliveryGuyUserId { get; set; }
        [ForeignKey("DeliveryGuyUserId")]
        [InverseProperty("Orders")]
        public virtual DeliveryGuy DeliveryGuyUser { get; set; }
 

        [Column("restaurantId")]
        public int RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        [InverseProperty("Orders")]
        public virtual Restaurant Restaurant { get; set; }

        [InverseProperty("Order")]
        public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
