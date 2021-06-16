using CustomerLibrary.BusinessLogic;
using CustomerLibrary.Data;
using CustomerLibrary.IntegrationTests.RepositoryTests;
using Xunit;

namespace CustomerLibrary.IntegrationTests.ProviderTests
{
    public class AddressProviderTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressProvider()
        {
            var customerRepository = new AddressProvider();
            Assert.NotNull(customerRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var fixture = new AddressProviderFixture();
            var mockAddressId = fixture.CreateMockAddress();
            Assert.NotEqual(0, mockAddressId);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            var addressProvider = new AddressProvider();
            var fixture = new AddressProviderFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressProvider.Read(addressId);

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
            var addressProvider = new AddressProvider();
            var fixture = new AddressProviderFixture();
            var addressId = fixture.CreateMockAddress();

            fixture.MockAddress.AddressLine = "Test";
            addressProvider.Update(fixture.MockAddress);
            var updatedAddress = addressProvider.Read(addressId);

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
            var addressProvider = new AddressProvider();
            var fixture = new AddressProviderFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressProvider.Read(addressId);

            Assert.NotNull(createdAddress);

            addressProvider.Delete(addressId);
            var deletedAddress = addressProvider.Read(addressId);

            Assert.Null(deletedAddress);
        }
    }

    public class AddressProviderFixture
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
            var addressProvider = new AddressProvider();

            addressRepository.DeleteAll();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            MockAddress.CustomerId = customerId;
            var newAddressId = addressProvider.Create(MockAddress);
            return newAddressId;
        }
    }
}