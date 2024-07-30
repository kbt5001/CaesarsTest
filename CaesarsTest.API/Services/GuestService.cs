using CaesarsTest.API.Entities;

namespace CaesarsTest.API.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly IGuestCacheService _guestCacheService;

        public GuestService(IGuestRepository guestRepository, IGuestCacheService guestCacheService)
        {
            _guestRepository = guestRepository;
            _guestCacheService = guestCacheService;
        }
        public async Task CreateGuest(Guest guest)
        {
            await _guestRepository.CreateGuestAsync(guest);
            
            if(await _guestRepository.SaveChangesAsync())
            {
                _guestCacheService.AddGuest(guest);
            }
        }

        public async Task DeleteGuest(Guest guest)
        {
            _guestRepository.DeleteGuest(guest);

            if(await _guestRepository.SaveChangesAsync())
            {
                _guestCacheService.DeleteGuest(guest.Id);
            }
        }

        public async Task<Guest> GetGuest(Guid guestId)
        {
            var guest = _guestCacheService.GetGuest(guestId);

            if (guest == null)
            {
                guest = await _guestRepository.GetGuestAsync(guestId);

                if (guest != null)
                    _guestCacheService.AddGuest(guest);
            }

            return guest;
        }

        public async Task<Guest> GetGuestFromDb(Guid guestId)
        {
            return await _guestRepository.GetGuestAsync(guestId);
        }

        public async Task<IEnumerable<Guest>> GetGuests()
        {
            return await _guestRepository.GetGuestsAsync();
        }

        public async Task UpdateGuest(Guest guest)
        {
            if (await _guestRepository.SaveChangesAsync())
            {
                var updatedGuest = await _guestRepository.GetGuestAsync(guest.Id);

                _guestCacheService.UpdateGuest(updatedGuest);
            }
        }
    }
}
