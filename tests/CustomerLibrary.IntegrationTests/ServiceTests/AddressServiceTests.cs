using CustomerLibrary.BusinessLogic;
using CustomerLibrary.Data;
using CustomerLibrary.IntegrationTests.RepositoryTests;
using Xunit;

namespace CustomerLibrary.IntegrationTests.ServiceTests
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
        public void ShouldBeAbleToCreateAddress()
        {
            var fixture = new AddressServiceFixture();
            var mockAddressId = fixture.CreateMockAddress();
            Assert.NotEqual(0, mockAddressId);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            var addressService = new AddressService();
            var fixture = new AddressServiceFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressService.Read(addressId);

            Assert.NotNull(createdAddress);
            Assert.Equal(fixture.MockAddress.AddressId, createdAddress.AddressId);
            Assert.Equal(fixture.MockAddress.CustomerId, createdAddress.CustomerId);
            Assert.Equal(fixture.MockAddress.AddressLine, createdAddress.AddressLine);
            Assert.Equal(fixture.MockAddress.AddressLine2, createdAddress.AddressLine2);
            Assert.Equal(fixture.MockAddress.AddressType, createdAddress.AddressType);
            Assert.Equal(fixture.MockAddress.Country, createdAddress.Country);
            Assert.Equal(fixture.MockAddress.City, createdAddress.City);
            Assert.Equal(fixture.MockAddress.State, createdAddress.State);
            Assert.Equal(fixture.MockAddress.PostalCode, createdAddress.PostalCode);
        }

        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            var addressService = new AddressService();
            var fixture = new AddressServiceFixture();
            var addressId = fixture.CreateMockAddress();

            fixture.MockAddress.AddressLine = "Test";
            addressService.Update(fixture.MockAddress);
            var updatedAddress = addressService.Read(addressId);

            Assert.NotNull(updatedAddress);
            Assert.Equal(fixture.MockAddress.AddressId, updatedAddress.AddressId);
            Assert.Equal(fixture.MockAddress.CustomerId, updatedAddress.CustomerId);
            Assert.Equal("Test", updatedAddress.AddressLine);
            Assert.Equal(fixture.MockAddress.AddressLine2, updatedAddress.AddressLine2);
            Assert.Equal(fixture.MockAddress.AddressType, updatedAddress.AddressType);
            Assert.Equal(fixture.MockAddress.Country, updatedAddress.Country);
            Assert.Equal(fixture.MockAddress.City, updatedAddress.City);
            Assert.Equal(fixture.MockAddress.State, updatedAddress.State);
            Assert.Equal(fixture.MockAddress.PostalCode, updatedAddress.PostalCode);
        }

        [Fact]
        public void ShouldBeAbleToDeleteAddress()
        {
            var addressService = new AddressService();
            var fixture = new AddressServiceFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressService.Read(addressId);

            Assert.NotNull(createdAddress);

            addressService.Delete(addressId);
            var deletedAddress = addressService.Read(addressId);

            Assert.Null(deletedAddress);
        }
    }

    public class AddressServiceFixture
    {
        public Address MockAddress { get; set; } = new Address
        {
            AddressLine = "75 PARK PLACE",
            AddressLine2 = "45 BROADWAY",
            AddressType = AddressTypes.Shipping,
            City = "New York",
            Country = "United States",
            State = "New York",
            PostalCode = "123456"
        };

        public int CreateMockAddress()
        {
            var addressRepository = new AddressRepository();
            var addressService = new AddressService();

            addressRepository.DeleteAll();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            MockAddress.CustomerId = customerId;
            var newAddressId = addressService.Create(MockAddress);
            return newAddressId;
        }
    }
}