using CustomerLibrary.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CustomerLibrary.Data.EFRepositories
{
    public class CustomerRepository : IMainRepository<Customer>
    {
        private readonly CustomerLibraryContext _context;

        public CustomerRepository()
        {
            _context = new CustomerLibraryContext();
        }

        public CustomerRepository(CustomerLibraryContext context)
        {
            _context = context;
        }

        public int Create(Customer customer)
        {
            var newCustomer = _context.Customers.Add(customer);

            _context.SaveChanges();

            return newCustomer.CustomerId;
        }

        public Customer Read(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public int Count()
        {
            return _context.Customers.Count();
        }

        public (List<Customer>, int) ReadPage(int offset, int limit)
        {
            return (_context.Customers.OrderByDescending(c => c.CustomerId).Skip(offset).Take(limit).ToList(), Count());
        }

        public void Update(Customer customer)
        {
            var dbCustomer = _context.Customers.Find(customer.CustomerId);

            if (dbCustomer != null)
            {
                _context.Entry(dbCustomer).CurrentValues.SetValues(customer);
            }
        }

        public void Delete(int customerId)
        {
            var customer = _context
                .Customers
                .Include("Addresses")
                .Include("Notes")
                .First(c => c.CustomerId == customerId);

            if (customer != null)
            {
                _context.Customers.Remove(customer);

                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            var customers = _context
                .Customers
                .Include("Addresses")
                .Include("Notes")
                .ToList();

            foreach (var customer in customers)
            {
                _context.Customers.Remove(customer);
            }

            _context.SaveChanges();
        }
    }
}