using Xunit;
using CustomerLibrary.ValidationAttributes;

namespace CustomerLibrary.Tests.ValidationAttributesTests
{
    public class CountryAttributeTests
    {
        [Fact]
        public void ShouldBeValid()
        {
            CountryAttribute countryAttribute = new CountryAttribute();

            Assert.True(countryAttribute.IsValid("United States"));
            Assert.True(countryAttribute.IsValid("Canada"));
            Assert.True(countryAttribute.IsValid(null));
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            CountryAttribute countryAttribute = new CountryAttribute();

            Assert.False(countryAttribute.IsValid("United"));
            Assert.False(countryAttribute.IsValid(""));
        }
    }
}