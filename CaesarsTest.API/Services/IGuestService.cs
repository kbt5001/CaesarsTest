using CaesarsTest.API.Entities;

namespace CaesarsTest.API.Services
{
    public interface IGuestService
    {
        public Task CreateGuest(Guest guest);

        public Task UpdateGuest(Guest guest);

        public Task DeleteGuest(Guest guest);
        
        public Task<Guest> GetGuest(Guid guestId);

        public Task<IEnumerable<Guest>> GetGuests();
    }
}
