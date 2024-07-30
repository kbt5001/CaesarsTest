using AutoMapper;
using CaesarsTest.API.Models;
using CaesarsTest.API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.JsonPatch;

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
        private readonly IValidator<GuestCreateUpdateDto> _validator;

        public GuestController(ILogger<GuestController> logger, IMapper mapper, IGuestService guestService, IValidator<GuestCreateUpdateDto> validator)
        {
            _logger = logger;
            _mapper = mapper;
            _guestService = guestService;
            _validator = validator;
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

        [Authorize(Policy = "MustBeAnAdmin")]
        [HttpPost]
        public async Task<ActionResult<GuestDto>> CreateGuest(
           GuestCreateUpdateDto guest)
        {
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(guest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

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
            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(guest);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var guestToUpdate = await _guestService.GetGuestFromDb(guestId);

            if (guestToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(guest, guestToUpdate);

            await _guestService.UpdateGuest(guestToUpdate);

            return NoContent();
        }

        [Authorize(Policy = "MustBeAnAdmin")]
        [HttpPatch("{guestid}")]
        public async Task<ActionResult> PartiallyUpdateGuest(Guid guestId,
            JsonPatchDocument<GuestCreateUpdateDto> patchDocument)
        {
            var guest = await _guestService.GetGuestFromDb(guestId);
            if (guest == null)
            {
                return NotFound();
            }

            var guestToPatch = _mapper.Map<GuestCreateUpdateDto>(
                guest);

            patchDocument.ApplyTo(guestToPatch);

            FluentValidation.Results.ValidationResult validationResult = await _validator.ValidateAsync(guestToPatch);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _mapper.Map(guestToPatch, guest);
            await _guestService.UpdateGuest(guest);

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
