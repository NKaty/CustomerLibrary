using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;
using Xunit;

namespace CustomerLibrary.IntegrationTests.ServiceTests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAndReadCustomerWithAddressesAndNotes()
        {
            var customerService = new CustomerService();
            var fixture = new CustomerServiceFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerService.Read(customerId);

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
            var fixture = new CustomerServiceFixture();
            fixture.MockCustomer.LastName = null;

            Assert.Throws<InvalidObjectException>(() => fixture.CreateMockCustomer());
            Assert.Equal(0, fixture.MockCustomer.CustomerId);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateCustomerWithInvalidAddresses()
        {
            var customerService = new CustomerService();
            var addressRepository = new AddressRepository();
            var noteRepository = new NoteRepository();
            var fixture = new CustomerServiceFixture();
            fixture.MockCustomer.Addresses[0].AddressLine = null;

            Assert.Throws<InvalidObjectException>(() => fixture.CreateMockCustomer());

            var createdCustomer = customerService.Read(fixture.MockCustomer.CustomerId);
            Assert.Null(createdCustomer);

            var addresses = addressRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(addresses);

            var notes = noteRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(notes);
        }

        [Fact]
        public void ShouldNotBeAbleToCreateCustomerWithAddressesAndInvalidNotes()
        {
            var customerService = new CustomerService();
            var addressRepository = new AddressRepository();
            var noteRepository = new NoteRepository();
            var fixture = new CustomerServiceFixture();
            fixture.MockCustomer.Notes[0].NoteText = null;

            Assert.Throws<SqlException>(() => fixture.CreateMockCustomer());

            var createdCustomer = customerService.Read(fixture.MockCustomer.CustomerId);
            Assert.Null(createdCustomer);

            var addresses = addressRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(addresses);

            var notes = noteRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(notes);
        }

        [Fact]
        public void ShouldBeAbleToUpdateCustomerWithAddressesAndNotes()
        {
            var customerService = new CustomerService();
            var fixture = new CustomerServiceFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerService.Read(customerId);

            createdCustomer.LastName = "test1";
            createdCustomer.Addresses[0].AddressLine = "test2";
            createdCustomer.Notes[0].NoteText = "test3";

            customerService.Update(createdCustomer);

            var updatedCustomer = customerService.Read(customerId);

            Assert.Equal("test1", updatedCustomer.LastName);
            Assert.Equal("test2", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("test3", updatedCustomer.Notes[0].NoteText);
        }


        [Fact]
        public void ShouldNotBeAbleToUpdateInvalidCustomer()
        {
            var customerService = new CustomerService();
            var fixture = new CustomerServiceFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerService.Read(customerId);

            createdCustomer.LastName = null;
            createdCustomer.Addresses[0].AddressLine = "test2";
            createdCustomer.Notes[0].NoteText = "test3";

            Assert.Throws<InvalidObjectException>(() => customerService.Update(createdCustomer));

            var updatedCustomer = customerService.Read(customerId);

            Assert.Equal("Smith", updatedCustomer.LastName);
            Assert.Equal("75 PARK PLACE", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("Note1", updatedCustomer.Notes[0].NoteText);
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateCustomerWithInvalidAddresses()
        {
            var customerService = new CustomerService();
            var fixture = new CustomerServiceFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerService.Read(customerId);

            createdCustomer.LastName = "test1";
            createdCustomer.Addresses[0].AddressLine = null;
            createdCustomer.Notes[0].NoteText = "test3";

            Assert.Throws<InvalidObjectException>(() => customerService.Update(createdCustomer));

            var updatedCustomer = customerService.Read(customerId);

            Assert.Equal("Smith", updatedCustomer.LastName);
            Assert.Equal("75 PARK PLACE", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("Note1", updatedCustomer.Notes[0].NoteText);
        }

        [Fact]
        public void ShouldNotBeAbleToUpdateCustomerWithAddressesAndInvalidNotes()
        {
            var customerService = new CustomerService();
            var fixture = new CustomerServiceFixture();
            var customerId = fixture.CreateMockCustomer();
            Assert.NotEqual(0, customerId);

            var createdCustomer = customerService.Read(customerId);

            createdCustomer.LastName = "test1";
            createdCustomer.Addresses[0].AddressLine = "test2";
            createdCustomer.Notes[0].NoteText = null;

            Assert.Throws<SqlException>(() => customerService.Update(createdCustomer));

            var updatedCustomer = customerService.Read(customerId);

            Assert.Equal("Smith", updatedCustomer.LastName);
            Assert.Equal("75 PARK PLACE", updatedCustomer.Addresses[0].AddressLine);
            Assert.Equal("Note1", updatedCustomer.Notes[0].NoteText);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomerWithAddressesAndNotes()
        {
            var customerService = new CustomerService();
            var fixture = new CustomerServiceFixture();
            var customerId = fixture.CreateMockCustomer();
            var createdCustomer = customerService.Read(customerId);

            Assert.NotNull(createdCustomer);
            Assert.Equal(fixture.MockCustomer.Addresses.Count, createdCustomer.Addresses.Count);
            Assert.Equal(fixture.MockCustomer.Notes.Count, createdCustomer.Notes.Count);

            customerService.Delete(createdCustomer.CustomerId);

            var deletedCustomer = customerService.Read(customerId);
            Assert.Null(deletedCustomer);

            var addressRepository = new AddressRepository();
            var noteRepository = new NoteRepository();

            var addresses = addressRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(addresses);

            var notes = noteRepository.ReadByCustomerId(fixture.MockCustomer.CustomerId);
            Assert.Empty(notes);
        }
    }

    public class CustomerServiceFixture
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
            Notes = new List<Note> {new Note {NoteText = "Note1"}, new Note {NoteText = "note2"}},
            TotalPurchasesAmount = 100.84M
        };

        public int CreateMockCustomer()
        {
            var customerRepository = new CustomerRepository();
            var customerService = new CustomerService();
            customerRepository.DeleteAll();

            var customerId = customerService.Create(MockCustomer);
            return customerId;
        }
    }
}