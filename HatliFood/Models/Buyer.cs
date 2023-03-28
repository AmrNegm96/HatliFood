using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HatliFood.Models
{
    public class Buyer : IPerson
    {
        [ForeignKey(nameof(User)), Key]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        HashSet<Address> Addresses = new HashSet<Address>();
    }

    public enum Gender
    {
        Male, Female
    }
}
