using CaesarsTest.API.DbContexts;
using CaesarsTest.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace CaesarsTest.API.Services
{
    public class GuestRepository : IGuestRepository
    {
        private readonly GuestContext _context;

        public GuestRepository(GuestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateGuestAsync(Guest guest)
        {
            await _context.Guests.AddAsync(guest);
        }

        public void DeleteGuest(Guest guest)
        {
            _context.Guests.Remove(guest);
        }

        public async Task<Guest?> GetGuestAsync(Guid guestId)
        {
            return await _context.Guests.Where(c => c.Id == guestId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Guest>> GetGuestsAsync()
        {
            return await _context.Guests.ToListAsync();
        }

        public async Task<bool> GuestExistsAsync(Guid guestId)
        {
            return await _context.Guests.AnyAsync(c => c.Id == guestId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
