using CustomerLibrary.Data.EFRepositories;
using Xunit;

namespace CustomerLibrary.IntegrationTests.EFRepositoryTests
{
    public class AddressRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateAddressRepository()
        {
            var addressRepository = new AddressRepository();
            Assert.NotNull(addressRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateAddress()
        {
            var fixture = new AddressRepositoryFixture();
            var mockAddressId = fixture.CreateMockAddress();
            Assert.NotEqual(0, mockAddressId);
        }

        [Fact]
        public void ShouldBeAbleToReadAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressRepository.Read(addressId);

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
        public void ShouldBeAbleToReadAddressesByCustomerId()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var customerId = fixture.CreateMockAddresses();
            var createdAddresses = addressRepository.ReadByCustomerId(customerId);

            Assert.Equal(2, createdAddresses.Count);
            Assert.Equal(customerId, createdAddresses[0].CustomerId);
            Assert.Equal(customerId, createdAddresses[1].CustomerId);
        }

        [Fact]
        public void ShouldBeAbleToCountAddressesByCustomerId()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var customerId = fixture.CreateMockAddresses();
            var count = addressRepository.CountByCustomerId(customerId);

            Assert.Equal(2, count);
        }

        [Fact]
        public void ShouldBeAbleToUpdateAddress()
        {
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var addressId = fixture.CreateMockAddress();

            fixture.MockAddress.AddressLine = "Test";
            addressRepository.Update(fixture.MockAddress);
            var updatedAddress = addressRepository.Read(addressId);

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
            var addressRepository = new AddressRepository();
            var fixture = new AddressRepositoryFixture();
            var addressId = fixture.CreateMockAddress();
            var createdAddress = addressRepository.Read(addressId);

            Assert.NotNull(createdAddress);

            addressRepository.Delete(addressId);
            var deletedAddress = addressRepository.Read(addressId);

            Assert.Null(deletedAddress);
        }
    }

    public class AddressRepositoryFixture
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

        public int CreateMockAddress(int customerId = 0)
        {
            var addressRepository = new AddressRepository();
            addressRepository.DeleteAll();

            if (customerId == 0)
            {
                var customerFixture = new CustomerRepositoryFixture();
                customerId = customerFixture.CreateMockCustomer();
            }

            MockAddress.CustomerId = customerId;
            var newAddressId = addressRepository.Create(MockAddress);
            return newAddressId;
        }

        public int CreateMockAddresses()
        {
            var addressRepository = new AddressRepository();
            addressRepository.DeleteAll();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            MockAddress.CustomerId = customerId;
            addressRepository.Create(MockAddress);
            addressRepository.Create(MockAddress);
            return customerId;
        }
    }
}
