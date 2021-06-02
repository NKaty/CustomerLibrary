using System;
using System.Collections.Generic;
using Xunit;

namespace CustomerLibrary.Tests
{
    public class CustomerValidatorTests
    {
        [Fact]
        public void ShouldReturnEmptyErrorListIfCustomerIsValid()
        {
            Address address1 = new Address()
            {
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "12345"
            };
            Address address2 = new Address()
            {
                AddressLine = "100 PARK PLACE",
                AddressLine2 = "866 BROADWAY",
                AddressType = AddressTypes.Billing,
                City = "Some city",
                Country = "Canada",
                State = "Some state",
                PostalCode = "34588"
            };
            Customer customer = new Customer()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Addresses = new List<Address>() { address1, address2 },
                Email = "bob@gmail.com",
                PhoneNumber = "",
                Notes = new List<string> { "Note" },
                TotalPurchasesAmount = 100.84M
            };

            var errors = CustomerValidator.Validate(customer);
            Assert.IsType<List<string>>(errors);
            Assert.Empty(errors);
        }

        [Fact]
        public void ShouldReturnListOfErrorsIfCustomerIsNotValid()
        {
            Customer customer = new Customer();

            List<string> errors = CustomerValidator.Validate(customer);
            Assert.IsType<List<string>>(errors);
            Assert.Equal(3, errors.Count);
        }

        [Fact]
        public void ShouldReturnListOfErrorsOfCustomerWithAddressErrorsIfAddressIsNotValid()
        {
            Customer customer = new Customer() { Addresses = new List<Address>() { new Address() } };

            List<string> errors = CustomerValidator.Validate(customer);
            Assert.IsType<List<string>>(errors);
            Assert.Equal(8, errors.Count);
        }

        [Fact]
        public void ShoulThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => CustomerValidator.Validate(null));
        }
    }
}
