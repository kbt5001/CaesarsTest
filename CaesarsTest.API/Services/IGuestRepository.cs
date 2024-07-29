using CaesarsTest.API.Entities;

namespace CaesarsTest.API.Services
{
    public interface IGuestRepository
    {
        Task<IEnumerable<Guest>> GetGuestsAsync();
        //Task<(IEnumerable<Guest>, PaginationMetadata)> GetGuestsAsync(
        //    string? name, string? searchQuery, int pageNumber, int pageSize);
        Task<Guest?> GetGuestAsync(Guid guestId);
        Task<bool> GuestExistsAsync(Guid guestId);
        Task CreateGuestAsync(Guest guest);
        void DeleteGuest(Guest guest);
        Task<bool> SaveChangesAsync();
    }
}
