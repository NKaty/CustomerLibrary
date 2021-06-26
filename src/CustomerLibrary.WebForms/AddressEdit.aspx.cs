using System;
using System.Linq;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class AddressEdit : System.Web.UI.Page
    {
        private readonly IService<Address> _addressService;

        public int CustomerId { get; set; }

        public AddressEdit()
        {
            _addressService = new AddressService();
        }

        public AddressEdit(IService<Address> addressService)
        {
            _addressService = addressService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (int.TryParse(Request.QueryString["addressId"], out var addressIdReq))
            {
                var address = LoadAddress(addressIdReq);

                customerId.Value = address.CustomerId.ToString();
                addressId.Value = address.AddressId.ToString();
                addressLine.Text = address.AddressLine;
                addressLine2.Text = address.AddressLine2;
                addressType.SelectedValue = address.AddressType.ToString();
                city.Text = address.City;
                postalCode.Text = address.PostalCode;
                state.Text = address.State;
                country.SelectedValue = address.Country;
            }
            else
            {
                if (int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
                {
                    customerId.Value = customerIdReq.ToString();
                    CustomerId = customerIdReq;
                }
            }
        }

        public Address LoadAddress(int addressIdReq)
        {
            var address = _addressService.Read(addressIdReq);
            CustomerId = address.CustomerId;

            return address;
        }

        public void SaveAddress(Address address)
        {
            if (address.AddressId != 0)
            {
                _addressService.Update(address);
            }
            else
            {
                _addressService.Create(address);
            }
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            if (int.TryParse(customerId?.Value, out var currentCustomerId))
            {
                var address = new Address
                {
                    CustomerId = currentCustomerId,
                    AddressLine = addressLine?.Text,
                    AddressLine2 = addressLine2?.Text,
                    AddressType = (AddressTypes) Enum.Parse(typeof(AddressTypes), addressType.SelectedItem.Value),
                    City = city?.Text,
                    PostalCode = postalCode?.Text,
                    State = state?.Text,
                    Country = country?.SelectedItem.Value
                };

                var validationResult = AddressValidator.Validate(address);

                if (validationResult.Count != 0)
                {
                    errors.DataSource = validationResult.Select(err => new {error = err});
                    errors.DataBind();
                    return;
                }

                if (int.TryParse(addressId?.Value, out var currentAddressId))
                {
                    address.AddressId = currentAddressId;
                }

                SaveAddress(address);

                Response?.Redirect($"AddressList?customerId={currentCustomerId}");
            }

            Response?.Redirect("CustomerList");
        }
    }
}