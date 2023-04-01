using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace HatliFood.Models
{
    public partial class Address
    {
        [Key]
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public int? Floor { get; set; }

        public int? ApartmentNumber { get; set; }


        [Column("BID")]
        public string BuyerID { get; set; }

        [ForeignKey("BuyerID")]
        [InverseProperty("Addresses")]
        public virtual Buyer? Buyer { get; set; }
    }
}
