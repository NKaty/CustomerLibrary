using CustomerLibrary.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CustomerLibrary.Data.EFRepositories
{
    public class AddressRepository : IDependentRepository<Address>
    {
        private readonly CustomerLibraryContext _context;

        public AddressRepository()
        {
            _context = new CustomerLibraryContext();
        }

        public AddressRepository(CustomerLibraryContext context)
        {
            _context = context;
        }

        public int Create(Address address)
        {
            var newAddress = _context.Addresses.Add(address);

            _context.SaveChanges();

            return newAddress.AddressId;
        }

        public Address Read(int addressId)
        {
            return _context.Addresses.Find(addressId);
        }

        public List<Address> ReadByCustomerId(int customerId)
        {
            return _context.Addresses.Where(a => a.CustomerId == customerId).ToList();
        }

        public int CountByCustomerId(int customerId)
        {
            return _context.Addresses.Count(a => a.CustomerId == customerId);
        }

        public void Update(Address address)
        {
            var dbAddress = _context.Addresses.Find(address.AddressId);

            if (dbAddress != null)
            {
                _context.Entry(dbAddress).CurrentValues.SetValues(address);

                _context.SaveChanges();
            }
        }

        public void Delete(int addressId)
        {
            var address = _context.Addresses.Find(addressId);

            if (address != null)
            {
                _context.Addresses.Remove(address);

                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            var addresses = _context.Addresses.ToList();

            foreach (var address in addresses)
            {
                _context.Addresses.Remove(address);
            }

            _context.SaveChanges();
        }
    }
}
