using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CaesarsTest.API.Entities
{
    public class HotelLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HotelLocationId { get; set; }
        public string LocationName { get; set; }
    }
}
