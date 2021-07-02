using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CustomerLibrary.ValidationAttributes;


namespace CustomerLibrary
{
    [Serializable]
    public class Address
    {
        public int AddressId { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("Address Line")]
        [Required(ErrorMessage = "Address line is required.")]
        [MaxLength(100, ErrorMessage = "Address line must be max 100 chars long.")]
        public string AddressLine { get; set; }

        [DisplayName("Address Line2")]
        [MaxLength(100, ErrorMessage = "Address line2 must be max 100 chars long.")]
        public string AddressLine2 { get; set; }

        [DisplayName("Address Type")]
        [Required(ErrorMessage = "Address type is required.")]
        [AddressTypes]
        public AddressTypes? AddressType { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(50, ErrorMessage = "City must be max 50 chars long.")]
        public string City { get; set; }

        [DisplayName("Postal Code")]
        [Required(ErrorMessage = "Postal code is required.")]
        [MaxLength(6, ErrorMessage = "Postal code must be max 6 chars long.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [MaxLength(20, ErrorMessage = "State must be max 20 chars long.")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [Country]
        public string Country { get; set; }
    }
}