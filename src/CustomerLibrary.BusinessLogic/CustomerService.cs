using System.Transactions;
using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data;

namespace CustomerLibrary.BusinessLogic
{
    public class CustomerService : IService<Customer>
    {
        private readonly IRepository<Customer> _customerRepository;

        private readonly IRepository<Address> _addressRepository;

        private readonly IRepository<Note> _noteRepository;

        private readonly IService<Address> _addressService;

        private readonly IService<Note> _noteService;

        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
            _addressRepository = new AddressRepository();
            _noteRepository = new NoteRepository();
            _addressService = new AddressService();
            _noteService = new NoteService();
        }

        public CustomerService(IRepository<Customer> customerRepository, IRepository<Address> addressRepository,
            IRepository<Note> noteRepository, IService<Address> addressService, IService<Note> noteService)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _noteRepository = noteRepository;
            _addressService = addressService;
            _noteService = noteService;
        }

        public int Create(Customer customer)
        {
            using TransactionScope scope = new TransactionScope();
            var customerId = _customerRepository.Create(customer);

            if (customerId == 0)
            {
                throw new NotCreatedException("Customer was not created.");
            }

            customer.CustomerId = customerId;

            foreach (var address in customer.Addresses)
            {
                address.CustomerId = customerId;
                var addressId = _addressService.Create(address);

                address.AddressId = addressId;
            }

            foreach (var note in customer.Notes)
            {
                note.CustomerId = customerId;
                var noteId = _noteService.Create(note);

                note.NoteId = noteId;
            }

            scope.Complete();

            return customerId;
        }

        public Customer Read(int customerId)
        {
            var customer = _customerRepository.Read(customerId);

            if (customer is null)
            {
                return null;
            }

            customer.Addresses = (_addressRepository as AddressRepository)?.ReadByCustomerId(customerId);
            customer.Notes = (_noteRepository as NoteRepository)?.ReadByCustomerId(customerId);

            return customer;
        }

        public void Update(Customer customer)
        {
            using TransactionScope scope = new TransactionScope();
            _customerRepository.Update(customer);

            foreach (var address in customer.Addresses)
            {
                _addressService.Update(address);
            }

            foreach (var note in customer.Notes)
            {
                _noteService.Update(note);
            }

            scope.Complete();
        }

        public void Delete(int customerId)
        {
            _customerRepository.Delete(customerId);
        }
    }
}