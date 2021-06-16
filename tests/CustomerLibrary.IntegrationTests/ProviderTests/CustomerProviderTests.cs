using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;
using CustomerLibrary.Data;
using Xunit;

namespace CustomerLibrary.IntegrationTests.ProviderTests
{
    public class CustomerProviderTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerProvider()
        {
            var customerRepository = new CustomerProvider();
            Assert.NotNull(customerRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAndReadCustomerWithAddressesAndNotes()
        {
            var customerProvider = new CustomerProvider();
            var fixture = new CustomerProviderFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerProvider.Read(customerId);

            Assert.Equal(fixture.MockCustomer.CustomerId, createdCustomer.CustomerId);
            Assert.Equal(fixture.MockCustomer.FirstName, createdCustomer.FirstName);
            Assert.Equal(fixture.MockCustomer.LastName, createdCustomer.LastName);
            Assert.Equal(fixture.MockCustomer.Email, createdCustomer.Email);
            Assert.Equal(fixture.MockCustomer.PhoneNumber, createdCustomer.PhoneNumber);
            Assert.Equal(fixture.MockCustomer.TotalPurchasesAmount, createdCustomer.TotalPurchasesAmount);
            Assert.Equal(fixture.MockCustomer.Addresses.Count, createdCustomer.Addresses.Count);
            Assert.Equal(fixture.MockCustomer.Notes.Count, createdCustomer.Notes.Count);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateInvalidCustomer()
        {
            var fixture = new CustomerProviderFixture();
            fixture.MockCustomer.LastName = null;
            var customerId = fixture.CreateMockCustomer();

            Assert.Equal(0, customerId);
            Assert.Equal(0, fixture.MockCustomer.CustomerId);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateCustomerWithInvalidAddresses()
        {
            var addressRepository = new AddressRepository();
            var noteRepository = new NoteRepository();
            var fixture = new CustomerProviderFixture();
            fixture.MockCustomer.Addresses[0].AddressLine = null;
            var customerId = fixture.CreateMockCustomer();
            Assert.Equal(0, customerId);

            var addresses = addressRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(addresses);

            var notes = noteRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(notes);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateCustomerWithAddressesAndInvalidNotes()
        {
            var addressRepository = new AddressRepository();
            var noteRepository = new NoteRepository();
            var fixture = new CustomerProviderFixture();
            fixture.MockCustomer.Notes[0].NoteText = null;
            var customerId = fixture.CreateMockCustomer();
            Assert.Equal(0, customerId);

            var addresses = addressRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(addresses);

            var notes = noteRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(notes);
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomerWithAddressesAndNotes()
        {
            var customerProvider = new CustomerProvider();
            var fixture = new CustomerProviderFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerProvider.Read(customerId);

            createdCustomer.LastName = "test1";
            createdCustomer.Addresses[0].AddressLine = "test2";
            createdCustomer.Notes[0].NoteText = "test3";

            customerProvider.Update(createdCustomer);

            var updatedCustomer = customerProvider.Read(customerId);

            Assert.Equal("test1", updatedCustomer.LastName);
            Assert.Equal("test2", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("test3", updatedCustomer.Notes[0].NoteText);
        }

        
        [Fact]
        public void ShouldNotBeAbleToUpdateInvalidCustomer()
        {
            var customerProvider = new CustomerProvider();
            var fixture = new CustomerProviderFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);
        
            var createdCustomer = customerProvider.Read(customerId);
        
            createdCustomer.LastName = null;
            createdCustomer.Addresses[0].AddressLine = "test2";
            createdCustomer.Notes[0].NoteText = "test3";
        
            customerProvider.Update(createdCustomer);
        
            var updatedCustomer = customerProvider.Read(customerId);

            Assert.Equal("Smith", updatedCustomer.LastName);
            Assert.Equal("75 PARK PLACE", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("Note1", updatedCustomer.Notes[0].NoteText);
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateCustomerWithInvalidAddresses()
        {
            var customerProvider = new CustomerProvider();
            var fixture = new CustomerProviderFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerProvider.Read(customerId);

            createdCustomer.LastName = "test1";
            createdCustomer.Addresses[0].AddressLine = null;
            createdCustomer.Notes[0].NoteText = "test3";

            customerProvider.Update(createdCustomer);

            var updatedCustomer = customerProvider.Read(customerId);

            Assert.Equal("Smith", updatedCustomer.LastName);
            Assert.Equal("75 PARK PLACE", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("Note1", updatedCustomer.Notes[0].NoteText);
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateCustomerWithAddressesAndInvalidNotes()
        {
            var customerProvider = new CustomerProvider();
            var fixture = new CustomerProviderFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerProvider.Read(customerId);

            createdCustomer.LastName = "test1";
            createdCustomer.Addresses[0].AddressLine = "test2";
            createdCustomer.Notes[0].NoteText = null;

            customerProvider.Update(createdCustomer);

            var updatedCustomer = customerProvider.Read(customerId);

            Assert.Equal("Smith", updatedCustomer.LastName);
            Assert.Equal("75 PARK PLACE", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("Note1", updatedCustomer.Notes[0].NoteText);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomerWithAddressesAndNotes()
        {
            var customerProvider = new CustomerProvider();
            var fixture = new CustomerProviderFixture();
            var customerId = fixture.CreateMockCustomer();
            var createdCustomer = customerProvider.Read(customerId);

            Assert.NotNull(createdCustomer);
            Assert.Equal(fixture.MockCustomer.Addresses.Count, createdCustomer.Addresses.Count);
            Assert.Equal(fixture.MockCustomer.Notes.Count, createdCustomer.Notes.Count);

            customerProvider.Delete(createdCustomer.CustomerId);

            var deletedCustomer = customerProvider.Read(customerId);
            Assert.Null(deletedCustomer);

            var addressRepository = new AddressRepository();
            var noteRepository = new NoteRepository();

            var addresses = addressRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(addresses);

            var notes = noteRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(notes);
        }
    }

    public class CustomerProviderFixture
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
                },
                new Address
                {
                    AddressLine = "100 PARK PLACE",
                    AddressLine2 = "866 BROADWAY",
                    AddressType = AddressTypes.Billing,
                    City = "Some city",
                    Country = "Canada",
                    State = "Some state",
                    PostalCode = "3459"
                }
            },
            Email = "bob@gmail.com",
            PhoneNumber = "+123456789",
            Notes = new List<Note> {new Note() {NoteText = "Note1"}, new Note() {NoteText = "note2"}},
            TotalPurchasesAmount = 100.84M
        };

        public int CreateMockCustomer()
        {
            var customerRepository = new CustomerRepository();
            var customerProvider = new CustomerProvider();
            customerRepository.DeleteAll();

            var customerId = customerProvider.Create(MockCustomer);
            return customerId;
        }
    }
}