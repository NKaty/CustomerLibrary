using Xunit;

namespace CustomerLibrary.Tests
{
    public class AddressTests
    {
        [Fact]
        public void ShouldCreateAddress()
        {
            Address address = new Address()
            {
                AddressLine = "75 PARK PLACE",
                AddressLine2 = "45 BROADWAY",
                AddressType = AddressTypes.Shipping,
                City = "New York",
                Country = "United States",
                State = "New York",
                PostalCode = "123456"
            };

            Assert.Equal("75 PARK PLACE", address.AddressLine);
            Assert.Equal("45 BROADWAY", address.AddressLine2);
            Assert.Equal(AddressTypes.Shipping, address.AddressType);
            Assert.Equal("New York", address.City);
            Assert.Equal("United States", address.Country);
            Assert.Equal("New York", address.State);
            Assert.Equal("123456", address.PostalCode);
        }
    }
}
