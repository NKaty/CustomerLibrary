using System;
using CustomerLibrary.BusinessLogic.Common;
using Xunit;

namespace CustomerLibrary.Tests.ServicesTests.Common
{
    public class InvalidObjectExceptionTests
    {
        [Fact]
        public void ShouldBeAbleToCreateSqlException()
        {
            var invalidObjectException = new InvalidObjectException();
            Assert.NotNull(invalidObjectException);
        }

        [Fact]
        public void ShouldBeAbleToCreateSqlExceptionWithMessage()
        {
            var invalidObjectException = new InvalidObjectException("Message");
            Assert.NotNull(invalidObjectException);
            Assert.Equal("Message", invalidObjectException.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateSqlExceptionWithMessageAndInnerException()
        {
            var innerException = new Exception();
            var invalidObjectException = new InvalidObjectException("Message", innerException);
            Assert.NotNull(invalidObjectException);
            Assert.Equal("Message", invalidObjectException.Message);
            Assert.Equal(innerException, invalidObjectException.InnerException);
        }
    }
}
