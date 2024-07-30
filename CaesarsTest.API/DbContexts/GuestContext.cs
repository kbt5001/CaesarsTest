using CaesarsTest.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaesarsTest.API.DbContexts
{
    public class GuestContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; } = null!;
        public DbSet<HotelLocation> HotelLocations { get; set; } = null!;

        public GuestContext(DbContextOptions<GuestContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelLocation>()
                .HasData(
                new HotelLocation()
                {
                    HotelLocationId = 1,
                    LocationName = "Atlantic City",
                },
                new HotelLocation()
                {
                    HotelLocationId = 2,
                    LocationName = "Las Vegas"
                });            

            modelBuilder.Entity<Guest>()
                .HasData(
               new Guest()
               {
                   Id = Guid.NewGuid(),
                   FirstName = "Frank",
                   LastName = "Johnson",
                   DateOfBirth = new DateTime(1980, 1, 12),
                   EmailAddress =  "fjohnson@kmail.com",
                   PhoneNumber = "1234567890",
                   Address1 = "345 Front St",
                   City = "Atlantic City",
                   StateCode = "NJ",
                   PostalCode = "08201",
                   ArrivalDate = new DateTime(2024, 8, 10),
                   DepartureDate = new DateTime(2024, 8, 15),
                   HotelLocationId = 1
               },
               new Guest()
               {
                   Id = Guid.NewGuid(),
                   FirstName = "Jim",
                   LastName = "Williams",
                   DateOfBirth = new DateTime(1962, 4, 2),
                   EmailAddress = "jwilliams@kmail.com",
                   PhoneNumber = "2345678901",
                   Address1 = "123 Main St",
                   Address2 = "Apt 101",
                   City = "Atlantic City",
                   StateCode = "NJ",
                   PostalCode = "08201",
                   ArrivalDate = new DateTime(2024, 9, 1),
                   DepartureDate = new DateTime(2024, 8, 5),
                   HotelLocationId = 1
               },
               new Guest()
               {
                   Id = Guid.NewGuid(),
                   FirstName = "Mary",
                   LastName = "Richards",
                   DateOfBirth = new DateTime(1990, 9, 29),
                   EmailAddress = "mrichards@kmail.com",
                   PhoneNumber = "3456789012",
                   Address1 = "789 Bridge St",
                   City = "Las Vegas",
                   StateCode = "NV",
                   PostalCode = "88901",
                   ArrivalDate = new DateTime(2024, 10, 12),
                   DepartureDate = new DateTime(2024, 10, 20),
                   HotelLocationId = 2
               });

            base.OnModelCreating(modelBuilder);
        }
    }
}
