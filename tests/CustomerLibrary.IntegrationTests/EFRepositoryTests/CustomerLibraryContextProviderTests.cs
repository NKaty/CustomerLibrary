using CustomerLibrary.Data.EFRepositories;
using Xunit;

namespace CustomerLibrary.IntegrationTests.EFRepositoryTests
{
    public class CustomerLibraryContextProviderTests
    {
        [Fact]
        public void ShouldBeAbleToCreateCustomerLibraryContextProvider()
        {
            var context = CustomerLibraryContextProvider.Current;
            Assert.NotNull(context);
        }

        [Fact]
        public void ShouldBeAbleToPassContextIntoCustomerLibraryContextProvider()
        {
            var context = new CustomerLibraryContext();
            CustomerLibraryContextProvider.Current = context;
            Assert.NotNull(CustomerLibraryContextProvider.Current);
        }
    }
}
