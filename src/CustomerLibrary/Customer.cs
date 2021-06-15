using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary
{
    public class Customer : Person
    {
        public int CustomerId { get; set; }

        public override string FirstName { get; set; }

        public override string LastName { get; set; }

        [Required(ErrorMessage = "Addresses is required.")]
        [MinLength(1, ErrorMessage = "There must be at least one address.")]
        public List<Address> Addresses { get; set; }

        [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Incorrect phone number.")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Incorrect email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Notes is required.")]
        [MinLength(1, ErrorMessage = "There must be at least one note.")]
        public List<Note> Notes { get; set; }

        public decimal? TotalPurchasesAmount { get; set; }
    }
}