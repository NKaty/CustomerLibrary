﻿using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data;

namespace CustomerLibrary.BusinessLogic
{
    public class AddressService : IService<Address>
    {
        private readonly IRepository<Address> _addressRepository;

        public AddressService()
        {
            _addressRepository = new AddressRepository();
        }

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public int Create(Address address)
        {
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
            _addressRepository.Update(address);
        }

        public void Delete(int addressId)
        {
            _addressRepository.Delete(addressId);
        }
    }
}