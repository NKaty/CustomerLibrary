using System;
using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data;
using Moq;
using Xunit;

namespace CustomerLibrary.Tests.ServicesTests
{
    public class CustomerServiceTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerService()
        {
            var customerService = new CustomerService();
            Assert.NotNull(customerService);
        }

        [Fact]
        public void ShouldCreateCustomerWithAddressesAndNotes()
        {
            var fixture = new CustomerServiceFixture();

            var customer = fixture.CreateCustomer();
            fixture.CustomerRepositoryMock.Setup(r => r.Create(customer)).Returns(1);

            fixture.AddressServiceMock.Setup(s => s.Create(fixture.MockAddress)).Returns(1);
            fixture.NoteServiceMock.Setup(s => s.Create(fixture.MockNote)).Returns(1);
            var service = fixture.CreateService();

            var customerId = service.Create(customer);
            Assert.Equal(1, customerId);

            fixture.CustomerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Create(fixture.MockAddress), Times.Exactly(2));
            fixture.NoteServiceMock.Verify(s => s.Create(fixture.MockNote), Times.Exactly(2));
        }

        [Fact]
        public void ShouldNotCreateInvalidCustomer()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Create(customer)).Throws<Exception>();

            fixture.AddressServiceMock.Setup(s => s.Create(fixture.MockAddress)).Returns(1);
            fixture.NoteServiceMock.Setup(s => s.Create(fixture.MockNote)).Returns(1);
            var service = fixture.CreateService();

            Assert.Throws<Exception>(() => service.Create(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Create(fixture.MockAddress), Times.Exactly(0));
            fixture.NoteServiceMock.Verify(s => s.Create(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldNotCreateCustomerWithInvalidAddress()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Create(customer)).Returns(1);

            fixture.AddressServiceMock.Setup(s => s.Create(fixture.MockAddress)).Throws<Exception>();
            fixture.NoteServiceMock.Setup(s => s.Create(fixture.MockNote)).Returns(1);
            var service = fixture.CreateService();

            Assert.Throws<Exception>(() => service.Create(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Create(fixture.MockAddress), Times.Exactly(1));
            fixture.NoteServiceMock.Verify(s => s.Create(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldNotCreateCustomerWithInvalidNote()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Create(customer)).Returns(1);

            fixture.AddressServiceMock.Setup(s => s.Create(fixture.MockAddress)).Returns(1);
            fixture.NoteServiceMock.Setup(s => s.Create(fixture.MockNote)).Throws<Exception>();
            var service = fixture.CreateService();

            Assert.Throws<Exception>(() => service.Create(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Create(fixture.MockAddress), Times.Exactly(2));
            fixture.NoteServiceMock.Verify(s => s.Create(fixture.MockNote), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowInvalidObjectExceptionOnTryingToCreateInvalidCustomer()
        {
            var fixture = new CustomerServiceFixture();

            var customer = fixture.CreateCustomer();
            customer.LastName = null;
            fixture.CustomerRepositoryMock.Setup(r => r.Create(customer)).Returns(1);

            fixture.AddressServiceMock.Setup(s => s.Create(fixture.MockAddress)).Returns(1);
            fixture.NoteServiceMock.Setup(s => s.Create(fixture.MockNote)).Returns(1);
            var service = fixture.CreateService();

            Assert.Throws<InvalidObjectException>(() => service.Create(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(0));
            fixture.AddressServiceMock.Verify(s => s.Create(fixture.MockAddress), Times.Exactly(0));
            fixture.NoteServiceMock.Verify(s => s.Create(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldThrowNotCreatedException()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Create(customer)).Returns(0);

            fixture.AddressServiceMock.Setup(s => s.Create(fixture.MockAddress)).Returns(1);
            fixture.NoteServiceMock.Setup(s => s.Create(fixture.MockNote)).Returns(1);
            var service = fixture.CreateService();

            Assert.Throws<NotCreatedException>(() => service.Create(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Create(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Create(fixture.MockAddress), Times.Exactly(0));
            fixture.NoteServiceMock.Verify(s => s.Create(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldReadPageOfCustomers()
        {
            var fixture = new CustomerServiceFixture();
            fixture.CustomerRepositoryMock.Setup(r => r.ReadPage(0, 1))
                .Returns(new List<Customer> {fixture.MockCustomer});
            var service = fixture.CreateService();

            var customers = service.ReadPage(0, 1);

            Assert.Equal(fixture.MockCustomer, customers[0]);

            fixture.CustomerRepositoryMock.Verify(r => r.ReadPage(0, 1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCountCustomers()
        {
            var fixture = new CustomerServiceFixture();
            fixture.CustomerRepositoryMock.Setup(r => r.Count())
                .Returns(1);
            var service = fixture.CreateService();

            var count = service.Count();

            Assert.Equal(1, count);

            fixture.CustomerRepositoryMock.Verify(r => r.Count(), Times.Exactly(1));
        }

        [Fact]
        public void ShouldReadCustomerWithAddressesAndNotes()
        {
            var fixture = new CustomerServiceFixture();
            fixture.CustomerRepositoryMock.Setup(r => r.Read(1)).Returns(fixture.MockCustomer);
            fixture.AddressRepositoryMock.Setup(s => s.ReadByCustomerId(1))
                .Returns(new List<Address> {fixture.MockAddress, fixture.MockAddress});
            fixture.NoteRepositoryMock.Setup(s => s.ReadByCustomerId(1))
                .Returns(new List<Note> {fixture.MockNote, fixture.MockNote});
            var service = fixture.CreateService();

            var customer = service.Read(1);

            Assert.Equal(fixture.MockCustomer, customer);
            Assert.Equal(fixture.MockAddress, customer.Addresses[0]);
            Assert.Equal(fixture.MockAddress, customer.Addresses[1]);
            Assert.Equal(fixture.MockNote, customer.Notes[0]);
            Assert.Equal(fixture.MockNote, customer.Notes[1]);

            fixture.CustomerRepositoryMock.Verify(r => r.Read(1), Times.Exactly(1));
            fixture.AddressRepositoryMock.Verify(r => r.ReadByCustomerId(1), Times.Exactly(1));
            fixture.NoteRepositoryMock.Verify(r => r.ReadByCustomerId(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldReturnNullIfNoCustomerToRead()
        {
            var fixture = new CustomerServiceFixture();
            Customer noCustomer = null;
            fixture.CustomerRepositoryMock.Setup(r => r.Read(1)).Returns(noCustomer);
            fixture.AddressRepositoryMock.Setup(s => s.ReadByCustomerId(1))
                .Returns(new List<Address> {fixture.MockAddress, fixture.MockAddress});
            fixture.NoteRepositoryMock.Setup(s => s.ReadByCustomerId(1))
                .Returns(new List<Note> {fixture.MockNote, fixture.MockNote});
            var service = fixture.CreateService();

            var customer = service.Read(1);

            Assert.Null(customer);

            fixture.CustomerRepositoryMock.Verify(r => r.Read(1), Times.Exactly(1));
            fixture.AddressRepositoryMock.Verify(r => r.ReadByCustomerId(1), Times.Exactly(0));
            fixture.NoteRepositoryMock.Verify(r => r.ReadByCustomerId(1), Times.Exactly(0));
        }

        [Fact]
        public void ShouldUpdateCustomerWithAddressesAndNotesThatExist()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Update(customer));

            fixture.AddressServiceMock.Setup(s => s.Update(fixture.MockAddress));
            fixture.NoteServiceMock.Setup(s => s.Update(fixture.MockNote));
            var service = fixture.CreateService();

            service.Update(customer);

            fixture.CustomerRepositoryMock.Verify(r => r.Update(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Update(fixture.MockAddress), Times.Exactly(2));
            fixture.NoteServiceMock.Verify(s => s.Update(fixture.MockNote), Times.Exactly(2));
        }

        [Fact]
        public void ShouldUpdateCustomerWithNewAddressesAndNotes()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();
            fixture.MockAddress.AddressId = 0;
            fixture.MockNote.NoteId = 0;

            fixture.CustomerRepositoryMock.Setup(r => r.Update(customer));

            var service = fixture.CreateService();

            service.Update(customer);

            fixture.AddressServiceMock.Verify(s => s.Create(It.IsAny<Address>()));
            fixture.NoteServiceMock.Verify(s => s.Create(It.IsAny<Note>()));
        }

        [Fact]
        public void ShouldNotUpdateInvalidCustomer()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Update(customer)).Throws<Exception>();

            fixture.AddressServiceMock.Setup(s => s.Update(fixture.MockAddress));
            fixture.NoteServiceMock.Setup(s => s.Update(fixture.MockNote));
            var service = fixture.CreateService();

            Assert.Throws<Exception>(() => service.Update(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Update(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Update(fixture.MockAddress), Times.Exactly(0));
            fixture.NoteServiceMock.Verify(s => s.Update(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldNotUpdateCustomerWithInvalidAddress()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Update(customer));

            fixture.AddressServiceMock.Setup(s => s.Update(fixture.MockAddress)).Throws<Exception>();
            fixture.NoteServiceMock.Setup(s => s.Update(fixture.MockNote));
            var service = fixture.CreateService();

            Assert.Throws<Exception>(() => service.Update(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Update(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Update(fixture.MockAddress), Times.Exactly(1));
            fixture.NoteServiceMock.Verify(s => s.Update(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldThrowInvalidObjectExceptionOnTryingToUpdateInvalidCustomer()
        {
            var fixture = new CustomerServiceFixture();

            var customer = fixture.CreateCustomer();
            customer.LastName = null;

            fixture.CustomerRepositoryMock.Setup(r => r.Update(customer));

            fixture.AddressServiceMock.Setup(s => s.Update(fixture.MockAddress));
            fixture.NoteServiceMock.Setup(s => s.Update(fixture.MockNote));
            var service = fixture.CreateService();

            Assert.Throws<InvalidObjectException>(() => service.Update(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Update(customer), Times.Exactly(0));
            fixture.AddressServiceMock.Verify(s => s.Update(fixture.MockAddress), Times.Exactly(0));
            fixture.NoteServiceMock.Verify(s => s.Update(fixture.MockNote), Times.Exactly(0));
        }

        [Fact]
        public void ShouldNotUpdateCustomerWithInvalidNote()
        {
            var fixture = new CustomerServiceFixture();
            var customer = fixture.CreateCustomer();

            fixture.CustomerRepositoryMock.Setup(r => r.Update(customer));

            fixture.AddressServiceMock.Setup(s => s.Update(fixture.MockAddress));
            fixture.NoteServiceMock.Setup(s => s.Update(fixture.MockNote)).Throws<Exception>();
            var service = fixture.CreateService();

            Assert.Throws<Exception>(() => service.Update(customer));

            fixture.CustomerRepositoryMock.Verify(r => r.Update(customer), Times.Exactly(1));
            fixture.AddressServiceMock.Verify(s => s.Update(fixture.MockAddress), Times.Exactly(2));
            fixture.NoteServiceMock.Verify(s => s.Update(fixture.MockNote), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCallRepositoryDelete()
        {
            var fixture = new CustomerServiceFixture();
            fixture.CustomerRepositoryMock.Setup(r => r.Delete(1));
            var service = fixture.CreateService();

            service.Delete(1);

            fixture.CustomerRepositoryMock.Verify(r => r.Delete(1), Times.Exactly(1));
        }

        public class CustomerServiceFixture
        {
            public Mock<IMainRepository<Customer>> CustomerRepositoryMock { get; set; }

            public Mock<IDependentRepository<Address>> AddressRepositoryMock { get; set; }

            public Mock<IDependentRepository<Note>> NoteRepositoryMock { get; set; }

            public Mock<IService<Address>> AddressServiceMock { get; set; }

            public Mock<IService<Note>> NoteServiceMock { get; set; }

            public Customer MockCustomer { get; set; } = new Customer
            {
                CustomerId = 1,
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob@gmail.com",
                PhoneNumber = "+123456789",
                TotalPurchasesAmount = 100.84M
            };

            public Address MockAddress { get; set; } = new Address
            {
                AddressId = 1,
                CustomerId = 1,
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };

            public Note MockNote { get; set; } = new Note
            {
                NoteId = 1,
                CustomerId = 1,
                NoteText = "Note1"
            };

            public CustomerServiceFixture()
            {
                CustomerRepositoryMock = new Mock<IMainRepository<Customer>>();
                AddressRepositoryMock = new Mock<IDependentRepository<Address>>();
                NoteRepositoryMock = new Mock<IDependentRepository<Note>>();
                AddressServiceMock = new Mock<IService<Address>>();
                NoteServiceMock = new Mock<IService<Note>>();
            }

            public Customer CreateCustomer()
            {
                MockCustomer.Addresses = new List<Address> {MockAddress, MockAddress};
                MockCustomer.Notes = new List<Note> {MockNote, MockNote};
                return MockCustomer;
            }

            public CustomerService CreateService()
            {
                return new CustomerService(CustomerRepositoryMock.Object, AddressRepositoryMock.Object,
                    NoteRepositoryMock.Object, AddressServiceMock.Object, NoteServiceMock.Object);
            }
        }
    }
}