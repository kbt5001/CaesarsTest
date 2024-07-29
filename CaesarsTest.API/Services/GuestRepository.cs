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
        private readonly IGuestCacheService _cache;

        public GuestRepository(GuestContext context, IGuestCacheService guestCache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = guestCache;
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

        //public async Task<(IEnumerable<Guest>, PaginationMetadata)> GetGuestsAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
        //{
        //    var collection = _context.Guests as IQueryable<Guest>;

        //    if (!string.IsNullOrWhiteSpace(name))
        //    {
        //        name = name.Trim();
        //        collection = collection.Where(c => $"{c.FirstName} {c.LastName}" == name);
        //    }

        //    var totalItemCount = await collection.CountAsync();

        //    var paginationMetadata = new PaginationMetadata(
        //        totalItemCount, pageSize, pageNumber);

        //    var collectionToReturn = await collection.OrderBy(c => c.FirstName)
        //        .Skip(pageSize * (pageNumber - 1))
        //        .Take(pageSize)
        //        .ToListAsync();

        //    return (collectionToReturn, paginationMetadata);
        //}

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
