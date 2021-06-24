using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class CustomerEdit : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public CustomerEdit()
        {
            _customerService = new CustomerService();
        }

        public CustomerEdit(IMainService<Customer> customerService)
        {
            _customerService = customerService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var customerIdReq = Request.QueryString["customerId"];

            LoadCustomer(customerIdReq);
        }

        public void LoadCustomer(string customerIdReq)
        {
            if (customerIdReq != null)
            {
                var customer = _customerService.Read(Convert.ToInt32(customerIdReq));

                firstName.Text = customer.FirstName;
                lastName.Text = customer.LastName;
                email.Text = customer.Email;
                phoneNumber.Text = customer.PhoneNumber;
                amount.Text = customer.TotalPurchasesAmount.ToString();

                addresses.DataSource = customer.Addresses;
                notes.DataSource = customer.Notes;
            }
            else
            {
                addresses.DataSource = new List<Address> {new Address()};
                notes.DataSource = new List<Note> {new Note()};
            }

            addresses.DataBind();
            notes.DataBind();
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            var customerIdReq = Request.QueryString["customerId"];

            var customer = new Customer()
            {
                FirstName = firstName?.Text,
                LastName = lastName?.Text,
                PhoneNumber = phoneNumber?.Text,
                Email = email?.Text,
                TotalPurchasesAmount = Convert.ToDecimal(amount?.Text)
            };

            var addressesList = new List<Address>();

            foreach (RepeaterItem address in addresses.Items)
            {
                var newAddress = new Address
                {
                    AddressLine = ((TextBox) addresses.Items[0].FindControl("addressLine"))?.Text,
                    AddressLine2 = ((TextBox) addresses.Items[0].FindControl("addressLine2"))?.Text,
                    AddressType = (AddressTypes) Enum.Parse(typeof(AddressTypes),
                        ((DropDownList) addresses.Items[0].FindControl("addressType")).SelectedItem.Value),
                    City = ((TextBox) addresses.Items[0].FindControl("city"))?.Text,
                    PostalCode = ((TextBox) addresses.Items[0].FindControl("postalCode"))?.Text,
                    State = ((TextBox) addresses.Items[0].FindControl("state"))?.Text,
                    Country = ((DropDownList) addresses.Items[0].FindControl("country"))?.SelectedItem.Value
                };

                if (customerIdReq != null)
                {
                    newAddress.CustomerId = Convert.ToInt32(customerIdReq);
                }

                var addressId = Convert.ToInt32(((TextBox) addresses.Items[0].FindControl("addressId"))?.Text);
                if (addressId != 0)
                {
                    newAddress.AddressId = addressId;
                }

                addressesList.Add(newAddress);
            }

            var notesList = new List<Note>();

            foreach (RepeaterItem address in addresses.Items)
            {
                var newNote = new Note
                {
                    NoteText = ((TextBox)notes.Items[0].FindControl("noteText"))?.Text,
                };

                if (customerIdReq != null)
                {
                    newNote.CustomerId = Convert.ToInt32(customerIdReq);
                }

                var noteId = Convert.ToInt32(((TextBox)notes.Items[0].FindControl("noteId"))?.Text);
                if (noteId != 0)
                {
                    newNote.NoteId = noteId;
                }

                notesList.Add(newNote);
            }

            customer.Addresses = addressesList;
            customer.Notes = notesList;

            if (customerIdReq == null)
            {
                _customerService.Create(customer);
            }
            else
            {
                customer.CustomerId = Convert.ToInt32(customerIdReq);
                _customerService.Update(customer);
            }

            Response?.Redirect("CustomerList");
        }

        protected void addresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                var s = source as List<Address>;
                //addresses.DataSource = s.ToList().Add(new Address());
            }
        }
    }
}