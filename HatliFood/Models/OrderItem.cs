using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HatliFood.Models
{
    [PrimaryKey("OrderId", "MenuItemId")]
    [Table("OrderItem")]
    public partial class OrderItem
    {
        [Key]
        public int OrderId { get; set; }

        [Key]
        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("MenuItemId")]
        [InverseProperty("OrderItems")]
        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OrderItems")]
        public virtual Order Order { get; set; }
    }
}
