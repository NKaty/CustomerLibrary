using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class AddressEditTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressEdit()
        {
            var addressEdit = new AddressEdit();

            Assert.NotNull(addressEdit);
        }

        [Fact]
        public void ShouldBeAbleToLoadAddress()
        {
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address {CustomerId = 10};
            addressServiceMock.Setup(s => s.Read(1)).Returns(address);

            var addressEdit = new AddressEdit(addressServiceMock.Object);
            addressEdit.LoadAddress(1);

            Assert.Equal(10, addressEdit.CustomerId);
        }

        [Fact]
        public void ShouldCreateAddress()
        {
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address();
            addressServiceMock.Setup(s => s.Create(address)).Returns(10);

            var addressEdit = new AddressEdit(addressServiceMock.Object);
            addressEdit.SaveAddress(address);

            addressServiceMock.Verify(s => s.Create(address), Times.Exactly(1));
        }

        [Fact]
        public void ShouldUpdateAddress()
        {
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address { AddressId = 10 };
            addressServiceMock.Setup(s => s.Update(address));

            var addressEdit = new AddressEdit(addressServiceMock.Object);
            addressEdit.SaveAddress(address);

            addressServiceMock.Verify(s => s.Update(address), Times.Exactly(1));
        }
    }
}
