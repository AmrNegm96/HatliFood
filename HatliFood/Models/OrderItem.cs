using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace HatliFood.Models
{
    [PrimaryKey("OrderId", "MenuItemId")]
    [Table("OrderItem")]
    public partial class OrderItem
    {
        public int OrderId { get; set; }

        public int MenuItemId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("MenuItemId")]
        [InverseProperty("MOrderItems")]
        public virtual MenuItem MenuItem { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("OOrderItems")]
        public virtual Order Order { get; set; }
    }
}
