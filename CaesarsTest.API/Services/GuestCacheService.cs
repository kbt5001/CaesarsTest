using CaesarsTest.API.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CaesarsTest.API.Services
{
    public class GuestCacheService : IGuestCacheService
    {
        private readonly IMemoryCache _cache;

        public GuestCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void AddGuest(Guest guest)
        {
            if (guest != null)
            {
                _cache.Set(guest.Id, guest, DefaultCachedOptions());
            }
        }

        public void DeleteGuest(Guid guestId)
        {
            _cache.Remove(guestId);
        }

        public Guest GetGuest(Guid guestId)
        {
            _cache.TryGetValue(guestId, out Guest cachedGuest);

            return cachedGuest;
        }

        public void UpdateGuest(Guest guest)
        {
            _cache.Set(guest.Id, guest, DefaultCachedOptions());
        }

        private MemoryCacheEntryOptions DefaultCachedOptions(int expirationInterval = 30)
        {
            return new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationInterval));
        }
    }
}
