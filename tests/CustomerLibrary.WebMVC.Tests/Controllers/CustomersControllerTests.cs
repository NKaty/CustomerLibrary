using System.Collections.Generic;
using System.Web.Mvc;
using CustomerLibrary.BusinessLogic;
using CustomerLibrary.WebMVC.Controllers;
using Moq;
using Xunit;


namespace CustomerLibrary.WebMVC.Tests.Controllers
{
    public class CustomersControllerTests
    {
        [Fact]
        public void ShouldCreateCustomersController()
        {
            var controller = new CustomersController();

            Assert.NotNull(controller);
        }

        [Fact]
        public void ShouldReturnListOfCustomersForPage()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();

            var customer = new Customer();

            var controller = new CustomersController(customerServiceMock.Object);
            customerServiceMock.Setup(s => s.ReadPage(0, controller.CustomersPerPage)).Returns((new List<Customer> {customer}, 1));

            var result = controller.Index(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(1, result.ViewBag.CurrentPage);
            Assert.Equal(1, result.ViewBag.LastPage);
        }

        [Fact]
        public void ShouldReturnViewToCreateCustomer()
        {
            var controller = new CustomersController();

            var result = controller.Create() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldCreateCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Address address = new Address
            {
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };
            Note note = new Note {NoteText = "Note1"};
            Customer customer = new Customer
            {
                FirstName = "Bob",
                LastName = "Smith",
                Addresses = new List<Address> {address},
                Email = "bob@gmail.com",
                PhoneNumber = "",
                Notes = new List<Note> {note},
                TotalPurchasesAmount = 100.84M
            };

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Create(customer) as RedirectToRouteResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldNotCreateInvalidCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer();

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Create(customer) as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnViewToEditCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var customer = new Customer() {CustomerId = 10, Addresses = new List<Address>(), Notes = new List<Note>()};

            customerServiceMock.Setup(s => s.Read(10)).Returns(customer);
            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Edit(10) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(10, (result.ViewData.Model as Customer)?.CustomerId);
        }

        [Fact]
        public void ShouldEditCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Address address = new Address
            {
                CustomerId = 1,
                AddressId = 1,
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };
            Note note = new Note {CustomerId = 1, NoteId = 1, NoteText = "Note1"};
            Customer customer = new Customer
            {
                CustomerId = 1,
                FirstName = "Bob",
                LastName = "Smith",
                Addresses = new List<Address> {address},
                Email = "bob@gmail.com",
                PhoneNumber = "",
                Notes = new List<Note> {note},
                TotalPurchasesAmount = 100.84M
            };

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Edit(1, customer) as RedirectToRouteResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldNotEditInvalidCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer();

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Edit(1, customer) as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldAddNewAddressToCustomerForm()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer {Addresses = new List<Address> {new Address()}};

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.AddAddress(1, customer) as ViewResult;

            Assert.Equal(2, customer.Addresses.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldDeleteLastAddressFromCustomerForm()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer {Addresses = new List<Address> {new Address(), new Address()}};

            var controller = new CustomersController(customerServiceMock.Object);
            controller.TempData["AddressesStartLength"] = 2;

            customer.Addresses.Add(new Address());

            var result = controller.DeleteAddress(1, customer) as ViewResult;

            Assert.Equal(2, customer.Addresses.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldNotDeleteLastAddressFromCustomerForm()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer { Addresses = new List<Address> { new Address(), new Address() } };

            var controller = new CustomersController(customerServiceMock.Object);
            controller.TempData["AddressesStartLength"] = 2;

            var result = controller.DeleteAddress(1, customer) as ViewResult;

            Assert.Equal(2, customer.Addresses.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldAddNewNoteToCustomerForm()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer { Notes = new List<Note> { new Note() } };

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.AddNote(1, customer) as ViewResult;

            Assert.Equal(2, customer.Notes.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldDeleteLastNoteFromCustomerForm()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer { Notes = new List<Note> { new Note(), new Note() } };

            var controller = new CustomersController(customerServiceMock.Object);
            controller.TempData["NotesStartLength"] = 2;

            customer.Notes.Add(new Note());

            var result = controller.DeleteNote(1, customer) as ViewResult;

            Assert.Equal(2, customer.Notes.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldNotDeleteLastNoteFromCustomerForm()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            Customer customer = new Customer { Notes = new List<Note> { new Note(), new Note() } };

            var controller = new CustomersController(customerServiceMock.Object);
            controller.TempData["NotesStartLength"] = 2;

            var result = controller.DeleteNote(1, customer) as ViewResult;

            Assert.Equal(2, customer.Notes.Count);
            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnViewToDeleteCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var customer = new Customer() {CustomerId = 10};

            customerServiceMock.Setup(s => s.Read(10)).Returns(customer);
            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Delete(10) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(10, (result.ViewData.Model as Customer)?.CustomerId);
        }

        [Fact]
        public void ShouldDeleteCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var customer = new Customer {CustomerId = 1};

            var controller = new CustomersController(customerServiceMock.Object);

            var result = controller.Delete(1, 1, customer) as RedirectToRouteResult;

            Assert.NotNull(result);
        }
    }
}