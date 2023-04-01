using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HatliFood.Models
{
    [Table("Category")]
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        //[Column("RID")]
        [ForeignKey("RidNavigation")]
        public string Rid { get; set; }

        //[InverseProperty("CidNavigation")]
        public virtual ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();

        //[InverseProperty("Categories")]

        [ValidateNever]
        public virtual Restaurant RidNavigation { get; set; }
    }
}
