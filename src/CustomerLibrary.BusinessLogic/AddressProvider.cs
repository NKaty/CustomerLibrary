using CustomerLibrary.Data;

namespace CustomerLibrary.BusinessLogic
{
    public class AddressProvider
    {
        private readonly AddressRepository _addressRepository = new();

        public int Create(Address address)
        {
            return _addressRepository.Create(address);
        }

        public Address Read(int addressId)
        {
            return _addressRepository.Read(addressId);
        }

        public void Update(Address address)
        {
            _addressRepository.Update(address);
        }

        public void Delete(int addressId)
        {
            _addressRepository.Delete(addressId);
        }
    }
}