using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EntityFrameworkCore.EncryptColumn.Attribute;

namespace CaesarsTest.API.Entities
{
    public class Guest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
    }
}
