using FluentValidation;

namespace CustomerLibrary
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName)
                .MaximumLength(50)
                .WithMessage("First name must be max 50 chars long.");

            RuleFor(customer => customer.LastName)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Last name is required.")
                .NotEmpty()
                .WithMessage("Last name should not be epmty.")
                .MaximumLength(50)
                .WithMessage("Last name must be max 50 chars long.");

            RuleFor(customer => customer.Addresses)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Adresses is required.")
                .Must(address => address.Count > 0)
                .WithMessage("There must be at least one address.");

            RuleForEach(customer => customer.Addresses).SetValidator(new AddressValidator());

            RuleFor(customer => customer.PhoneNumber)
               .Matches(@"^\+[1-9]\d{1,14}$")
               .WithMessage("Incorrect phone number.");

            RuleFor(customer => customer.Email)
               .EmailAddress()
               .WithMessage("Incorrect email.");

            RuleFor(customer => customer.Notes)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Notes is required.")
                .Must(address => address.Count > 0)
                .WithMessage("There must be at least one note.");
        }
    }
}
