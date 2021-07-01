using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data;
using System.Transactions;

namespace CustomerLibrary.BusinessLogic
{
    public class AddressService : IDependentService<Address>
    {
        private readonly IDependentRepository<Address> _addressRepository;

        public AddressService()
        {
            _addressRepository = new AddressRepository();
        }

        public AddressService(IDependentRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public int Create(Address address)
        {
            var errors = AddressValidator.Validate(address);

            if (errors.Count != 0)
            {
                throw new InvalidObjectException($"Address is invalid. {string.Join(" ", errors)}");
            }

            var addressId = _addressRepository.Create(address);

            if (addressId == 0)
            {
                throw new NotCreatedException("Address was not created.");
            }

            return addressId;
        }

        public Address Read(int addressId)
        {
            return _addressRepository.Read(addressId);
        }

        public void Update(Address address)
        {
            var errors = AddressValidator.Validate(address);

            if (errors.Count != 0)
            {
                throw new InvalidObjectException($"Address is invalid. {string.Join(" ", errors)}");
            }

            _addressRepository.Update(address);
        }

        public void Delete(Address address)
        {
            using var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions()
            {
                IsolationLevel = IsolationLevel.Serializable
            });

            if (_addressRepository.CountByCustomerId(address.CustomerId) < 2)
            {
                throw new NotDeletedException("Cannot delete the only address.");
            }

            _addressRepository.Delete(address.AddressId);

            scope.Complete();
        }
    }
}