using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class CustomerDeleteTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerDelete()
        {
            var customerDelete = new CustomerDelete();

            Assert.NotNull(customerDelete);
        }

        [Fact]
        public void ShouldBeAbleToSetCustomerToDelete()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var customer = new Customer();
            customerServiceMock.Setup(s => s.Read(1)).Returns(customer);

            var customerDelete = new CustomerDelete(customerServiceMock.Object);
            customerDelete.SetCustomer(1);

            Assert.Equal(customer, customerDelete.CustomerToDelete);
        }

        [Fact]
        public void ShouldBeAbleToDeleteCustomer()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            customerServiceMock.Setup(s => s.Delete(1));

            var customerDelete = new CustomerDelete(customerServiceMock.Object);
            customerDelete.DeleteCustomer(1);

            customerServiceMock.Verify(s => s.Delete(1), Times.Exactly(1));
        }
    }
}
