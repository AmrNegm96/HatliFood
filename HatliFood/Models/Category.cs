using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HatliFood.Models
{
    [Table("Category")]
    public partial class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Column("RID")]
        public int Rid { get; set; }

        [InverseProperty("CidNavigation")]
        public virtual ICollection<MenuItem> MenuItems { get; } = new List<MenuItem>();

        [ForeignKey("Rid")]
        [InverseProperty("Categories")]
        public virtual Restaurant RidNavigation { get; set; }
    }
}
