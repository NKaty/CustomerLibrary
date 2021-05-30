using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibrary.Tests
{
    public class AddressValidatorTests
    {
        [Fact]
        public void ShouldReturnEmptyErrorListIfAddressIsValid()
        {
            Address address = new Address()
            {
                AddressLine = "Street",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123457"
            };

            List<string> errors = AddressValidator.Validate(address);
            Assert.IsType<List<string>>(errors);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldReturnListOfErrorsIfAddressIsNotValid()
        {
            Address address = new Address();

            List<string> errors = AddressValidator.Validate(address);
            Assert.IsType<List<string>>(errors);
            Assert.Equal(6, errors.Count);
        }

        [Fact]
        public void ShoulThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => AddressValidator.Validate(null));
        }
    }
}
