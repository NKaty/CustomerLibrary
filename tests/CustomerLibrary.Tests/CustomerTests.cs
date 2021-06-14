using System.Collections.Generic;
using Xunit;

namespace CustomerLibrary.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void ShouldCreateCustomer()
        {
            Address address1 = new Address()
            {
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };
            Address address2 = new Address()
            {
                AddressLine = "100 PARK PLACE",
                AddressLine2 = "866 BROADWAY",
                AddressType = AddressTypes.Billing,
                City = "Some city",
                Country = "Canada",
                State = "Some state",
                PostalCode = "3459"
            };
            Customer customer = new Customer()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Addresses = new List<Address>() {address1, address2},
                Email = "bob@gmail.com",
                PhoneNumber = "",
                Notes = new List<string> {"Note"},
                TotalPurchasesAmount = 100.84M
            };


            Assert.Equal("75 PARK PLACE", customer.Addresses[0].AddressLine);
            Assert.Equal("45 BROADWAY", customer.Addresses[0].AddressLine2);
            Assert.Equal(AddressTypes.Shipping, customer.Addresses[0].AddressType);
            Assert.Equal("New York", customer.Addresses[0].City);
            Assert.Equal("United States", customer.Addresses[0].Country);
            Assert.Equal("New York", customer.Addresses[0].State);
            Assert.Equal("123456", customer.Addresses[0].PostalCode);


            Assert.Equal("100 PARK PLACE", customer.Addresses[1].AddressLine);
            Assert.Equal("866 BROADWAY", customer.Addresses[1].AddressLine2);
            Assert.Equal(AddressTypes.Billing, customer.Addresses[1].AddressType);
            Assert.Equal("Some city", customer.Addresses[1].City);
            Assert.Equal("Canada", customer.Addresses[1].Country);
            Assert.Equal("Some state", customer.Addresses[1].State);
            Assert.Equal("3459", customer.Addresses[1].PostalCode);

            Assert.Equal("Bob", customer.FirstName);
            Assert.Equal("Smith", customer.LastName);
            Assert.Equal(address1, customer.Addresses[0]);
            Assert.Equal(address2, customer.Addresses[1]);
            Assert.Equal("bob@gmail.com", customer.Email);
            Assert.Equal("", customer.PhoneNumber);
            Assert.Equal("Note", customer.Notes[0]);
            Assert.Equal(100.84M, customer.TotalPurchasesAmount);
        }
    }
}