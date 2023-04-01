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
        public DateTime OrderDate { get; set; }= DateTime.Now;

        [EnumDataType(typeof(OrderStatus))]
        public OrderStatus OrderState { get; set; } = OrderStatus.Pending;


        [Column("BID")]
        public string BuyerId { get; set; }

        [ForeignKey("BuyerId")]
        [InverseProperty("Orders")]
        public virtual Buyer Buyer { get; set; }

        [Column("deliveryGuyId")]
        public string? DeliveryGuyUserId { get; set; }
        [ForeignKey("DeliveryGuyUserId")]
        [InverseProperty("DOrders")]
        public virtual DeliveryGuy? DeliveryGuyUser { get; set; }
 

        [Column("restaurantId")]
        public string RestaurantId { get; set; }
        [ForeignKey("RestaurantId")]
        [InverseProperty("ROrders")]
        public virtual Restaurant Restaurant { get; set; }

        public virtual ICollection<OrderItem> OOrderItems { get; set; } = new HashSet<OrderItem>();
    }

    public enum OrderStatus
    {
        Pending,
        Prepering,
        Delivering,
        Delivered
    }


}
