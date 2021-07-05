using System.Collections.Generic;
using CustomerLibrary.Data.EFRepositories;
using Xunit;

[assembly: CollectionBehavior(MaxParallelThreads = 1)]
namespace CustomerLibrary.IntegrationTests.EFRepositoryTests
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateEfCustomerRepository()
        {
            var countryRepository = new CustomerRepository();
            Assert.NotNull(countryRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomerRepositoryWithContext()
        {
            var context = new CustomerLibraryContext();
            var countryRepository = new CustomerRepository(context);
            Assert.NotNull(countryRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateCustomer()
        {
            var fixture = new CustomerRepositoryFixture();
            var mockCustomerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, mockCustomerId);
        }

        [Fact]
        public void ShouldBeAbleToCountCustomers()
        {
            var customerRepository = new CustomerRepository();
            var fixture = new CustomerRepositoryFixture(); 
            fixture.CreateMockCustomer();
            var count = customerRepository.Count();

            Assert.Equal(1, count);
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
        public void ShouldBeAbleToReadPageOfCustomers()
        {
            var customerRepository = new CustomerRepository();
            var fixture = new CustomerRepositoryFixture();
            fixture.CreateMockCustomer();
            var data = customerRepository.ReadPage(0, 1);
            var customers = data.Item1;
            var count = data.Item2;

            Assert.NotEmpty(customers);
            Assert.Equal(1, count);
            Assert.Equal(fixture.MockCustomer.CustomerId, customers[0].CustomerId);
            Assert.Equal(fixture.MockCustomer.FirstName, customers[0].FirstName);
            Assert.Equal(fixture.MockCustomer.LastName, customers[0].LastName);
            Assert.Equal(fixture.MockCustomer.Email, customers[0].Email);
            Assert.Equal(fixture.MockCustomer.PhoneNumber, customers[0].PhoneNumber);
            Assert.Equal(fixture.MockCustomer.TotalPurchasesAmount, customers[0].TotalPurchasesAmount);
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
            var deletedCustomer = customerRepository.Read(customerId);
            var deletedAddress = addressRepository.Read(addressId);

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
            Addresses = new List<Address>
            {
                new Address
                {
                    AddressLine = "75 PARK PLACE",
                    AddressLine2 = "45 BROADWAY",
                    AddressType = AddressTypes.Shipping,
                    City = "New York",
                    Country = "United States",
                    State = "New York",
                    PostalCode = "123456"
                }
            },
            Email = "bob@gmail.com",
            PhoneNumber = "+123456789",
            Notes = new List<Note>
            {
                new Note {NoteText = "Note1"}
            },
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