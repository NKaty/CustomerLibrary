using System.Collections.Generic;
using CustomerLibrary.ValidationAttributes;
using Xunit;

namespace CustomerLibrary.Tests.ValidationAttributesTests
{
    public class MinLengthListAttributeTests
    {
        [Fact]
        public void ShouldBeValid()
        {
            var minLengthListAttribute = new MinLengthListAttribute(1);

            Assert.True(minLengthListAttribute.IsValid(new List<int> {1, 2}));
            Assert.True(minLengthListAttribute.IsValid(new List<int> {1}));
        }

        [Fact]
        public void ShouldBeNotValid()
        {
            var minLengthListAttribute = new MinLengthListAttribute(2);

            Assert.False(minLengthListAttribute.IsValid(new List<int>()));
            Assert.False(minLengthListAttribute.IsValid(new List<int> {1}));
            Assert.False(minLengthListAttribute.IsValid(null));
        }
    }
}