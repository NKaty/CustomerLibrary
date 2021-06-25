using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class AddressListTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressList()
        {
            var addressList = new AddressList();

            Assert.NotNull(addressList);
        }

        [Fact]
        public void ShouldBeAbleToGetCustomerWithAddresses()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            customerServiceMock.Setup(s => s.Read(1))
                .Returns(() => new Customer() {Addresses = new List<Address> {new Address()}});

            var addressList = new AddressList(customerServiceMock.Object);
            addressList.LoadCustomerFromDatabase(1);

            Assert.NotNull(addressList.Customer);
            Assert.Single(addressList.Customer.Addresses);
        }
    }
}