using FluentValidation;

namespace CaesarsTest.API.Models.Validations
{
    public class GuestValidator : AbstractValidator<GuestCreateUpdateDto>
    {
        public GuestValidator() 
        {
            RuleFor(address => address.Address1)
              .NotEmpty()
              .WithMessage("Address 1 is required.");

            RuleFor(address => address.City)
              .NotEmpty()
              .WithMessage("City is required.");

            RuleFor(address => address.StateCode)
              .NotEmpty()
              .Length(2)
              .WithMessage("State Code is required.");

            RuleFor(address => address.PostalCode)
              .NotEmpty()
              .Matches(@"^\d{5}(-\d{4})?$")
              .WithMessage("Invalid Postal code.");

            RuleFor(guest => guest.FirstName)
              .NotEmpty()
              .WithMessage("First name is required.");

            RuleFor(guest => guest.LastName)
              .NotEmpty()
              .WithMessage("Last name is required.");

            RuleFor(guest => guest.PhoneNumber)
              .NotEmpty()
              .Matches(@"^\d{10}$")
              .WithMessage("Invalid phone number.");

            RuleFor(guest => guest.EmailAddress).NotEmpty().EmailAddress();

            RuleFor(guest => guest.DateOfBirth).NotEmpty();
        }
    }
}
