using System.Web.Mvc;
using CustomerLibrary.BusinessLogic;
using CustomerLibrary.WebMVC.Controllers;
using Moq;
using Xunit;

namespace CustomerLibrary.WebMVC.Tests.Controllers
{
    public class AddressesControllerTests
    {
        [Fact]
        public void ShouldCreateAddressesController()
        {
            var controller = new AddressesController();

            Assert.NotNull(controller);
        }

        [Fact]
        public void ShouldReturnListOfAddresses()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var addressServiceMock = new Mock<IService<Address>>();
            var customer = new Customer() {CustomerId = 1, FirstName = "Bob", LastName = "Smith"};

            customerServiceMock.Setup(s => s.Read(1)).Returns(customer);
            var controller = new AddressesController(customerServiceMock.Object, addressServiceMock.Object);

            var result = controller.Index(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Bob", result.ViewData["FirstName"]);
            Assert.Equal("Smith", result.ViewData["LastName"]);
        }

        [Fact]
        public void ShouldReturnViewToCreateAddress()
        {
            var controller = new AddressesController();

            var result = controller.Create(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(1, (result.ViewData.Model as Address)?.CustomerId);
        }

        [Fact]
        public void ShouldCreateAddress()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address();

            var controller = new AddressesController(customerServiceMock.Object, addressServiceMock.Object);

            var result = controller.Create(address) as RedirectToRouteResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnViewToEditAddress()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address() {CustomerId = 10};

            addressServiceMock.Setup(s => s.Read(1)).Returns(address);
            var controller = new AddressesController(customerServiceMock.Object, addressServiceMock.Object);

            var result = controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(10, (result.ViewData.Model as Address)?.CustomerId);
        }

        [Fact]
        public void ShouldEditAddress()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address();

            var controller = new AddressesController(customerServiceMock.Object, addressServiceMock.Object);

            var result = controller.Edit(address) as RedirectToRouteResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnViewToDeleteAddress()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address() { CustomerId = 10 };

            addressServiceMock.Setup(s => s.Read(1)).Returns(address);
            var controller = new AddressesController(customerServiceMock.Object, addressServiceMock.Object);

            var result = controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(10, (result.ViewData.Model as Address)?.CustomerId);
        }

        [Fact]
        public void ShouldDeleteAddress()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address {AddressId = 1};

            var controller = new AddressesController(customerServiceMock.Object, addressServiceMock.Object);

            var result = controller.Delete(1, address) as RedirectToRouteResult;

            Assert.NotNull(result);
        }
    }
}
