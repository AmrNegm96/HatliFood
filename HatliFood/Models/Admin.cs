using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HatliFood.Models
{
    [Table("Admin")]
    public partial class Admin : IPerson
    {
        [ForeignKey(nameof(User)), Key]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

    }
}
