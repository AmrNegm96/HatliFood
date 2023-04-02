using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HatliFood.Models.ViewModels
{
    public class RegisterResVM
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; }

        public string Email { get; set; }
        public string? Details { get; set; }

        public string? Location { get; set; }

        public string? City { get; set; }
        [Required]
        public string ImgPath { get; set; }

        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}