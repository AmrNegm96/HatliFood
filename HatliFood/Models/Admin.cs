using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HatliFood.Models
{
    [Table("Admin")]
    public partial class Admin : IPerson
    {
        [Key]
        public string UserId { get; set; }
    }
}
