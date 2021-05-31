using FluentValidation;

namespace CustomerLibrary
{
    public class AddressValidator : AbstractValidator<Address>
    {
        public AddressValidator()
        {
            RuleFor(address => address.AddressLine)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Address line is required.")
                .MaximumLength(100)
                .WithMessage("Address line must be max 100 chars long.");

            RuleFor(address => address.AddressLine2)
                .MaximumLength(100)
                .WithMessage("Address line2 must be max 100 chars long.");

            RuleFor(address => address.AddressType)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Address type is required.")
                .IsInEnum()
                .WithMessage("Address type must be Shipping or Billing.");

            RuleFor(address => address.City)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("City is required.")
                .MaximumLength(50)
                .WithMessage("City must be max 50 chars long.");

            RuleFor(address => address.PostalCode)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Postal code is required.")
                .MaximumLength(6)
                .WithMessage("Postal code must be max 6 chars long.");

            RuleFor(address => address.State)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("State is required.")
                .MaximumLength(20)
                .WithMessage("State must be max 20 chars long.");

            RuleFor(address => address.Country)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Country is required.")
                .Must(country => country == "United States" || country == "Canada")
                .WithMessage("Country must be United States or Canada.");
        }
    }
}
