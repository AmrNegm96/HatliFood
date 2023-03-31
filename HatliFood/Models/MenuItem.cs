using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
        [NotMapped]
         public IFormFile ImgFile { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        //[Column("CID")]
        [ForeignKey("CidNavigation")]
        public int Cid { get; set; }

        //[InverseProperty("MenuItems")]
        [ValidateNever]

        public virtual Category CidNavigation { get; set; }

        //[InverseProperty("MenuItem")]
        public virtual ICollection<OrderItem> MOrderItems { get; set; } = new HashSet<OrderItem>();
    }
}
