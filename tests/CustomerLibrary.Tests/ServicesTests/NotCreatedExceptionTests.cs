using System;
using System.Runtime.Serialization;
using CustomerLibrary.BusinessLogic.Common;
using Xunit;

namespace CustomerLibrary.Tests.ServicesTests
{
    public class NotCreatedExceptionTests
    {
        [Fact]
        public void ShouldBeAbleToCreateSqlException()
        {
            var sqlException = new NotCreatedException();
            Assert.NotNull(sqlException);
        }

        [Fact]
        public void ShouldBeAbleToCreateSqlExceptionWithMessage()
        {
            var sqlException = new NotCreatedException("Message");
            Assert.NotNull(sqlException);
            Assert.Equal("Message", sqlException.Message);
        }

        [Fact]
        public void ShouldBeAbleToCreateSqlExceptionWithMessageAndInnerException()
        {
            var innerException = new Exception();
            var sqlException = new NotCreatedException("Message", innerException);
            Assert.NotNull(sqlException);
            Assert.Equal("Message", sqlException.Message);
            Assert.Equal(innerException, sqlException.InnerException);
        }
    }
}
