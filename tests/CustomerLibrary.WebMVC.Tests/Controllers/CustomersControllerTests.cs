using System;
using CustomerLibrary.WebMVC.Controllers;
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
        public void ShouldReturnListOfCustomers()
        {
            var controller = new CustomersController();

            controller.Index();
        }
    }
}
