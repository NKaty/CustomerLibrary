using System;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class AddressDelete : System.Web.UI.Page
    {
        private readonly IService<Address> _addressService;

        public Address AddressToDelete { get; set; }

        public AddressDelete()
        {
            _addressService = new AddressService();
        }

        public AddressDelete(IService<Address> addressService)
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

        public void DeleteAddress(int customerId)
        {
            _addressService.Delete(customerId);
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            var addressId = GetAddressId();

            if (addressId != 0)
            {
                DeleteAddress(addressId);
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