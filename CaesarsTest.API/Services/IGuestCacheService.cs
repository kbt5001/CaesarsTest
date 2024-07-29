using CaesarsTest.API.Entities;

namespace CaesarsTest.API.Services
{
    public interface IGuestCacheService
    {
        public Guest GetGuest(Guid guestId);

        public void AddGuest(Guest guest);

        public void UpdateGuest(Guest guest);

        public void DeleteGuest(Guid guestId);
    }
}
