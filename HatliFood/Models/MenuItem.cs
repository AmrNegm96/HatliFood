using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HatliFood.Models
{
    [Table("MenuItem")]
    
    public partial class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string ImgPath { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Column("CID")]
        public int Cid { get; set; }

        [ForeignKey("Cid")]
        [InverseProperty("MenuItems")]
        public virtual Category CidNavigation { get; set; }

        [InverseProperty("MenuItem")]
        public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();
    }
}
