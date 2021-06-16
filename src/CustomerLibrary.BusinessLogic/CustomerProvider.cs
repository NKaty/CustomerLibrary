using System;
using CustomerLibrary.Data;

namespace CustomerLibrary.BusinessLogic
{
    public class CustomerProvider
    {
        private readonly CustomerRepository _customerRepository = new();

        private readonly AddressRepository _addressRepository = new();

        private readonly NoteRepository _noteRepository = new();

        public int Create(Customer customer)
        {
            using var connection = _customerRepository.GetConnection();
            var transaction = _customerRepository.GetTransaction(connection);

            int customerId;

            try
            {
                customerId = _customerRepository.Create(customer, connection, transaction);

                if (customerId == 0)
                {
                    transaction.Rollback();
                    return 0;
                }

                customer.CustomerId = customerId;

                foreach (var address in customer.Addresses)
                {
                    address.CustomerId = customerId;
                    var addressId = _addressRepository.Create(address, connection, transaction);

                    if (addressId == 0)
                    {
                        transaction.Rollback();
                        return 0;
                    }

                    address.AddressId = addressId;
                }

                foreach (var note in customer.Notes)
                {
                    note.CustomerId = customerId;
                    var noteId = _noteRepository.Create(note, connection, transaction);

                    if (noteId == 0)
                    {
                        transaction.Rollback();
                        return 0;
                    }

                    note.NoteId = noteId;
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                return 0;
            }

            return customerId;
        }

        public Customer Read(int customerId)
        {
            using var connection = _customerRepository.GetConnection();

            var customer = _customerRepository.Read(customerId, connection);

            if (customer is null)
            {
                return null;
            }

            customer.Addresses = _addressRepository.ReadByCustomerId(customerId, connection);
            customer.Notes = _noteRepository.ReadByCustomerId(customerId, connection);

            return customer;
        }

        public void Update(Customer customer)
        {
            using var connection = _customerRepository.GetConnection();
            var transaction = _customerRepository.GetTransaction(connection);

            try
            {
                _customerRepository.Update(customer, connection, transaction);

                foreach (var address in customer.Addresses)
                {
                    _addressRepository.Update(address, connection, transaction);
                }

                foreach (var note in customer.Notes)
                {
                    _noteRepository.Update(note, connection, transaction);
                }

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public void Delete(int customerId)
        {
            _customerRepository.Delete(customerId);
        }
    }
}