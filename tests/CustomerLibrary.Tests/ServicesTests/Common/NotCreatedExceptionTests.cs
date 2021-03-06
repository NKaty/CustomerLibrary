using System;
using CustomerLibrary.BusinessLogic.Common;
using Xunit;

namespace CustomerLibrary.Tests.ServicesTests.Common
{
    public class NotCreatedExceptionTests
    {
        [Fact]
        public void ShouldBeAbleToCreateException()
        {
            var exception = new NotCreatedException();
            Assert.NotNull(exception);
        }

        [Fact]
        public void ShouldBeAbleToCreateExceptionWithMessage()
        {
            var exception = new NotCreatedException("Message");
            Assert.NotNull(exception);
            Assert.Equal("Message", exception.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateExceptionWithMessageAndInnerException()
        {
            var innerException = new Exception();
            var exception = new NotCreatedException("Message", innerException);
            Assert.NotNull(exception);
            Assert.Equal("Message", exception.Message);
            Assert.Equal(innerException, exception.InnerException);
        }
    }
}
