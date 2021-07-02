using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data.Interfaces;
using Moq;
using Xunit;

namespace CustomerLibrary.Tests.ServicesTests
{
    public class AddressServiceTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressService()
        {
            var addressService = new AddressService();
            Assert.NotNull(addressService);
        }

        [Fact]
        public void ShouldCallRepositoryCreate()
        {
            var fixture = new AddressServiceFixture();
            fixture.AddressRepositoryMock.Setup(r => r.Create(fixture.MockAddress)).Returns(1);
            var service = fixture.CreateService();

            var addressId = service.Create(fixture.MockAddress);
            Assert.Equal(1, addressId);

            fixture.AddressRepositoryMock.Verify(r => r.Create(fixture.MockAddress), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowInvalidObjectExceptionOnTryingToCreateInvalidAddress()
        {
            var fixture = new AddressServiceFixture();
            fixture.MockAddress.AddressLine = null;
            fixture.AddressRepositoryMock.Setup(r => r.Create(fixture.MockAddress)).Returns(1);
            var service = fixture.CreateService();

            Assert.Throws<InvalidObjectException>(() => service.Create(fixture.MockAddress));

            fixture.AddressRepositoryMock.Verify(r => r.Create(fixture.MockAddress), Times.Exactly(0));
        }

        [Fact]
        public void ShouldThrowNotCreatedException()
        {
            var fixture = new AddressServiceFixture();
            fixture.AddressRepositoryMock.Setup(r => r.Create(fixture.MockAddress)).Returns(0);
            var service = fixture.CreateService();

            Assert.Throws<NotCreatedException>(() => service.Create(fixture.MockAddress));

            fixture.AddressRepositoryMock.Verify(r => r.Create(fixture.MockAddress), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCallRepositoryRead()
        {
            var fixture = new AddressServiceFixture();
            fixture.AddressRepositoryMock.Setup(r => r.Read(1)).Returns(fixture.MockAddress);
            var service = fixture.CreateService();

            var address = service.Read(1);
            Assert.Equal(fixture.MockAddress, address);

            fixture.AddressRepositoryMock.Verify(r => r.Read(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCallRepositoryUpdate()
        {
            var fixture = new AddressServiceFixture();
            fixture.AddressRepositoryMock.Setup(r => r.Update(fixture.MockAddress));
            var service = fixture.CreateService();

            service.Update(fixture.MockAddress);

            fixture.AddressRepositoryMock.Verify(r => r.Update(fixture.MockAddress), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowInvalidObjectExceptionOnTryingToUpdateInvalidAddress()
        {
            var fixture = new AddressServiceFixture();
            fixture.MockAddress.AddressLine = null;
            fixture.AddressRepositoryMock.Setup(r => r.Update(fixture.MockAddress));
            var service = fixture.CreateService();

            Assert.Throws<InvalidObjectException>(() => service.Update(fixture.MockAddress));

            fixture.AddressRepositoryMock.Verify(r => r.Update(fixture.MockAddress), Times.Exactly(0));
        }

        [Fact]
        public void ShouldCallRepositoryDelete()
        {
            var fixture = new AddressServiceFixture();
            var address = new Address {AddressId = 1, CustomerId = 1};
            fixture.AddressRepositoryMock.Setup(r => r.CountByCustomerId(1)).Returns(2);
            fixture.AddressRepositoryMock.Setup(r => r.Delete(1));
            var service = fixture.CreateService();

            service.Delete(address);

            fixture.AddressRepositoryMock.Verify(r => r.Delete(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldNotDeleteSingleAddress()
        {
            var fixture = new AddressServiceFixture();
            var address = new Address { AddressId = 1, CustomerId = 1 };
            fixture.AddressRepositoryMock.Setup(r => r.CountByCustomerId(1)).Returns(1);
            var service = fixture.CreateService();

            Assert.Throws<NotDeletedException>(() => service.Delete(address));
        }

        public class AddressServiceFixture
        {
            public Mock<IDependentRepository<Address>> AddressRepositoryMock { get; set; }

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

            public AddressServiceFixture()
            {
                AddressRepositoryMock = new Mock<IDependentRepository<Address>>();
            }

            public AddressService CreateService()
            {
                return new AddressService(AddressRepositoryMock.Object);
            }
        }
    }
}