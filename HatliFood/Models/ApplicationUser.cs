using Microsoft.AspNetCore.Identity;

namespace HatliFood.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }

    }
}
