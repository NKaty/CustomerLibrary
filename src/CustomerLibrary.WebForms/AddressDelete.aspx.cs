using System;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class AddressDelete : System.Web.UI.Page
    {
        private readonly IDependentService<Address> _addressService;

        public Address AddressToDelete { get; set; }

        public AddressDelete()
        {
            _addressService = new AddressService();
        }

        public AddressDelete(IDependentService<Address> addressService)
        {
            _addressService = addressService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var addressId = GetAddressId();

            SetAddress(addressId);
        }

        private int GetAddressId()
        {
            int.TryParse(Request.QueryString["addressId"], out var addressIdReq);

            return addressIdReq;
        }

        public void SetAddress(int addressId)
        {
            AddressToDelete = addressId == 0 ? null : _addressService.Read(addressId);
        }

        public void DeleteAddress(Address address)
        {
            _addressService.Delete(address);
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            var addressId = GetAddressId();

            if (addressId != 0)
            {
                SetAddress(addressId);
                DeleteAddress(AddressToDelete);
            }

            if (int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
            {
                Response?.Redirect($"AddressList?customerId={customerIdReq}");
            }
            else
            {
                Response?.Redirect("CustomerList");
            }
        }
    }
}