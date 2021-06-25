using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class AddressDeleteTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressDelete()
        {
            var addressDelete = new AddressDelete();

            Assert.NotNull(addressDelete);
        }

        [Fact]
        public void ShouldBeAbleToSetAddressToDelete()
        {
            var addressServiceMock = new Mock<IService<Address>>();
            var address = new Address();
            addressServiceMock.Setup(s => s.Read(1)).Returns(address);

            var addressDelete = new AddressDelete(addressServiceMock.Object);
            addressDelete.SetAddress(1);

            Assert.Equal(address, addressDelete.AddressToDelete);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        {
            var addressServiceMock = new Mock<IService<Address>>();
            addressServiceMock.Setup(s => s.Delete(1));

            var addressDelete = new AddressDelete(addressServiceMock.Object);
            addressDelete.DeleteAddress(1);

            addressServiceMock.Verify(s => s.Delete(1), Times.Exactly(1));
        }
    }
}

