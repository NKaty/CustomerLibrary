using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class CustomerListTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerList()
        {
            var customerList = new CustomerList();

            Assert.NotNull(customerList);
        }

        [Fact]
        public void ShouldBeAbleToLoadPageOfCustomers()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            customerServiceMock.Setup(s => s.ReadPage(0, 2))
                .Returns(() => new List<Customer> {new Customer(), new Customer()});

            var customerList = new CustomerList(customerServiceMock.Object);
            customerList.LoadCustomersFromDatabase(0, 2);

            Assert.Equal(2, customerList.Customers.Count);
        }

        [Fact]
        public void ShouldBeAbleToSetPagination()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            customerServiceMock.Setup(s => s.Count()).Returns(30);

            var customerList = new CustomerList(customerServiceMock.Object);
            var offset = customerList.SetPagination(2);

            Assert.Equal(customerList.CustomersPerPage, offset);
            Assert.Equal(2, customerList.CurrentPage);
        }

        [Fact]
        public void ShouldBeAbleToSetPaginationForPageWithoutRequestParam()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            customerServiceMock.Setup(s => s.Count()).Returns(30);

            var customerList = new CustomerList(customerServiceMock.Object);
            var offset = customerList.SetPagination(0);

            Assert.Equal(0, offset);
            Assert.Equal(1, customerList.CurrentPage);
        }
    }
}