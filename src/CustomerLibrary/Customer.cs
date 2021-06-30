using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CustomerLibrary.ValidationAttributes;

namespace CustomerLibrary
{
    [Serializable]
    public class Customer : Person
    {
        public int CustomerId { get; set; }

        [DisplayName("First Name")]
        public override string FirstName { get; set; }

        [DisplayName("Last Name")]
        public override string LastName { get; set; }

        [Required(ErrorMessage = "Addresses is required.")]
        [MinLengthList(1, ErrorMessage = "There must be at least one address.")]
        public List<Address> Addresses { get; set; }

        [DisplayName("Phone")]
        [RegularExpression(@"^\+[1-9]\d{1,14}$", ErrorMessage = "Incorrect phone number.")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Incorrect email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Notes is required.")]
        [MinLengthList(1, ErrorMessage = "There must be at least one note.")]
        public List<Note> Notes { get; set; }


        [DisplayName("Total Purchases Amount")]
        public decimal? TotalPurchasesAmount { get; set; }
    }
}