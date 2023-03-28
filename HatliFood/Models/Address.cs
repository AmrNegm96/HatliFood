using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HatliFood.Models
{
    public partial class Address
    {
        [Key]
        public string Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public int? Floor { get; set; }

        public int? ApartmentNumber { get; set; }

        [Required]
        [Column("BID")]
        public string Bid { get; set; }
    }
}
