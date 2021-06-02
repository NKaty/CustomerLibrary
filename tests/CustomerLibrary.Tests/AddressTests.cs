using System.Collections.Generic;
using Xunit;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary.Tests
{
    public class AddressTests
    {
        [Fact]
        public void ShouldCreateAddress()
        {
            Address address = new Address()
            {
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };

            Assert.Equal("75 PARK PLACE", address.AddressLine);
            Assert.Equal("45 BROADWAY", address.AddressLine2);
            Assert.Equal(AddressTypes.Shipping, address.AddressType);
            Assert.Equal("New York", address.City);
            Assert.Equal("United States", address.Country);
            Assert.Equal("New York", address.State);
            Assert.Equal("123456", address.PostalCode);
        }

        [Fact]
        public void ShouldAddressLineThrowRequiredError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressLine" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Address line is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldAddressLineThrowMaxLengthError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressLine" };
            bool isValid = Validator.TryValidateProperty("75 PARK PLACE 75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE", context, errors);
            Assert.False(isValid);
            Assert.Equal("Address line must be max 100 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldAddressLineBeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressLine" };
            bool isValid = Validator.TryValidateProperty("75 PARK PLACE", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldAddressLine2ThrowMaxLengthError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressLine2" };
            bool isValid = Validator.TryValidateProperty("75 PARK PLACE 75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE", context, errors);
            Assert.False(isValid);
            Assert.Equal("Address line2 must be max 100 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldAddressLine2BeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressLine2" };
            bool isValid = Validator.TryValidateProperty("75 PARK PLACE", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldAddressLine2BeValidWithoutValue()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressLine2" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldAddressTypeThrowRequiredError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressType" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Address type is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldAddressTypeBeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "AddressType" };
            bool isValid = Validator.TryValidateProperty(AddressTypes.Shipping, context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldCityThrowRequiredError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "City" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("City is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldCityThrowMaxLengthError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "City" };
            bool isValid = Validator.TryValidateProperty("75 PARK PLACE 75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARK PLACE75 PARKARK PLACE75 PARK PLACE75 PARK PLACE", context, errors);
            Assert.False(isValid);
            Assert.Equal("City must be max 50 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldCityBeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "City" };
            bool isValid = Validator.TryValidateProperty("New York", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldPostalCodeThrowRequiredError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "PostalCode" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Postal code is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldPostalCodeThrowMaxLengthError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "PostalCode" };
            bool isValid = Validator.TryValidateProperty("1234567", context, errors);
            Assert.False(isValid);
            Assert.Equal("Postal code must be max 6 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldPostalCodeBeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "PostalCode" };
            bool isValid = Validator.TryValidateProperty("12345", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldStateThrowRequiredError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "State" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("State is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldStateThrowMaxLengthError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "State" };
            bool isValid = Validator.TryValidateProperty("New York New York New York New York", context, errors);
            Assert.False(isValid);
            Assert.Equal("State must be max 20 chars long.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldStateBeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "State" };
            bool isValid = Validator.TryValidateProperty("New York", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldCountryThrowRequiredError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "Country" };
            bool isValid = Validator.TryValidateProperty(null, context, errors);
            Assert.False(isValid);
            Assert.Equal("Country is required.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldCountryThrowCountryError()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "Country" };
            bool isValid = Validator.TryValidateProperty("New York", context, errors);
            Assert.False(isValid);
            Assert.Equal("Country must be United States or Canada.", errors[0].ErrorMessage);
        }

        [Fact]
        public void ShouldCountryBeValid()
        {
            Address address = new Address();

            var errors = new List<ValidationResult>();
            var context = new ValidationContext(address) { MemberName = "Country" };
            bool isValid = Validator.TryValidateProperty("Canada", context, errors);
            Assert.True(isValid);
            Assert.Empty(errors);
        }
    }
}
