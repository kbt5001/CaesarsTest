using AutoMapper;
using CaesarsTest.API.Models;
using CaesarsTest.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CaesarsTest.API.Controllers
{
    [Route("api/v{version:apiVersion}/guests/")]
    [Authorize]
    [ApiVersion("1.0")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly ILogger<GuestController> _logger;
        private readonly IMapper _mapper;
        private readonly IGuestService _guestService;

        public GuestController(ILogger<GuestController> logger, IMapper mapper, IGuestService guestService)
        {
            _logger = logger;
            _mapper = mapper;
            _guestService = guestService;
        }

        [HttpGet("{guestid}", Name = "GetGuest")]
        public async Task<ActionResult<GuestDto>> GetGuest(Guid guestId)
        {
            var guest = await _guestService.GetGuest(guestId);

            if (guest == null)
            {
                _logger.LogInformation(
                    $"Guest with id {guestId} wasn't found.");
                return NotFound();
            }

            return Ok(_mapper.Map<GuestDto>(guest));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDto>>> GetGuests()
        {
            var guests = await _guestService.GetGuests();

            return Ok(_mapper.Map<IEnumerable<GuestDto>>(guests));
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GuestDto>>> GetGuests(
        //    string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        //{
        //    var (guestEntities, paginationMetadata) = await _guestRepository
        //        .GetGuestsAsync(name, searchQuery, pageNumber, pageSize);

        //    Response.Headers.Append("X-Pagination",
        //        JsonSerializer.Serialize(paginationMetadata));

        //    return Ok(_mapper.Map<IEnumerable<GuestDto>>(guestEntities));
        //}

        [Authorize(Policy = "MustBeAnAdmin")]
        [HttpPost]
        public async Task<ActionResult<GuestDto>> CreateGuest(
           GuestCreateUpdateDto guest)
        {
            var guestToCreate = _mapper.Map<Entities.Guest>(guest);

            await _guestService.CreateGuest(guestToCreate);

            var createdGuest =
                _mapper.Map<Models.GuestDto>(guestToCreate);

            return CreatedAtRoute("GetGuest",
                 new
                 {
                     guestId = createdGuest.Id
                 },
                 createdGuest);
        }

        [Authorize(Policy = "MustBeAnAdmin")]
        [HttpPut("{guestid}")]
        public async Task<ActionResult> UpdateGuest(Guid guestId,
            GuestCreateUpdateDto guest)
        {
            var guestToUpdate = await _guestService.GetGuest(guestId);

            if (guestToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(guest, guestToUpdate);

            await _guestService.UpdateGuest(guestToUpdate);

            return NoContent();
        }

        [Authorize(Policy = "MustBeAnAdmin")]
        [HttpDelete("{guestid}")]
        public async Task<ActionResult> DeleteGuest(Guid guestId)
        {
            var guest = await _guestService.GetGuest(guestId);

            if (guest == null)
            {
                return NotFound();
            }

            await _guestService.DeleteGuest(guest);
            

            return NoContent();
        }
    }
}
