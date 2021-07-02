using System.Data.Entity;

namespace CustomerLibrary.Data.EFRepositories
{
    public class CustomerLibraryContext : DbContext
    {
        public CustomerLibraryContext() : base("CustomersDB")
        {
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Note> Notes { get; set; }
    }
}