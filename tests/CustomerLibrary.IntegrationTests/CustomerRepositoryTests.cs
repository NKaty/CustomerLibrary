using System.Collections.Generic;
using Xunit;
using CustomerLibrary.Data;

namespace CustomerLibrary.IntegrationTests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerRepository()
        {
            var customerRepository = new CustomerRepository();
            Assert.NotNull(customerRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var fixture = new CustomerRepositoryFixture();
            var mockCustomerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, mockCustomerId);
        }

        [Fact]
        public void ShouldBeAbleToReadCustomer()
        {
            var customerRepository = new CustomerRepository();
            var fixture = new CustomerRepositoryFixture();
            var customerId = fixture.CreateMockCustomer();
            var createdCustomer = customerRepository.Read(customerId);

            Assert.NotNull(createdCustomer);
            Assert.Equal(fixture.MockCustomer.CustomerId, createdCustomer.CustomerId);
            Assert.Equal(fixture.MockCustomer.FirstName, createdCustomer.FirstName);
            Assert.Equal(fixture.MockCustomer.LastName, createdCustomer.LastName);
            Assert.Equal(fixture.MockCustomer.Email, createdCustomer.Email);
            Assert.Equal(fixture.MockCustomer.PhoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(fixture.MockCustomer.TotalPurchasesAmount, createdCustomer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomer()
        {
            var customerRepository = new CustomerRepository();
            var fixture = new CustomerRepositoryFixture();
            var customerId = fixture.CreateMockCustomer();

            fixture.MockCustomer.FirstName = "Test";
            customerRepository.Update(fixture.MockCustomer);
            var updatedCustomer = customerRepository.Read(customerId);

            Assert.NotNull(updatedCustomer);
            Assert.Equal("Test", updatedCustomer.FirstName);
            Assert.Equal(fixture.MockCustomer.CustomerId, updatedCustomer.CustomerId);
            Assert.Equal(fixture.MockCustomer.LastName, updatedCustomer.LastName);
            Assert.Equal(fixture.MockCustomer.Email, updatedCustomer.Email);
            Assert.Equal(fixture.MockCustomer.PhoneNumber, updatedCustomer.PhoneNumber);
            Assert.Equal(fixture.MockCustomer.TotalPurchasesAmount, updatedCustomer.TotalPurchasesAmount);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var customerRepository = new CustomerRepository();
            var fixtureCustomer = new CustomerRepositoryFixture();
            var customerId = fixtureCustomer.CreateMockCustomer();
            var createdCustomer = customerRepository.Read(customerId);
        
            var addressRepository = new AddressRepository();
            var fixtureAddress = new AddressRepositoryFixture();
            var addressId = fixtureAddress.CreateMockAddress(customerId);
            var createdAddress = addressRepository.Read(addressId);
        
            Assert.NotNull(createdCustomer);
            Assert.NotNull(createdAddress);
            Assert.Equal(customerId, createdAddress.CustomerId);
        
            customerRepository.Delete(customerId);
            var deletedAddress = addressRepository.Read(addressId);
            var deletedCustomer = customerRepository.Read(customerId);
        
            Assert.Null(deletedCustomer);
            Assert.Null(deletedAddress);
        }
    }

    public class CustomerRepositoryFixture
    {
        public Customer MockCustomer { get; set; } = new Customer
        {
            FirstName = "Bob",
            LastName = "Smith",
            Addresses = new List<Address>(),
            Email = "bob@gmail.com",
            PhoneNumber = "+123456789",
            Notes = new List<Note>(),
            TotalPurchasesAmount = 100.84M
        };

        public int CreateMockCustomer()
        {
            var customerRepository = new CustomerRepository();
            customerRepository.DeleteAll();

            var newCustomerId = customerRepository.Create(MockCustomer);
            return newCustomerId;
        }
    }
}