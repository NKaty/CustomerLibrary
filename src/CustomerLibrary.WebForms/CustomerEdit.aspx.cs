using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            if (!decimal.TryParse(amount?.Text, out var totalPurchasesAmount))
            {
                amountError.Text = "Total purchases amount must be a number.";
                return;
            }

            var customer = new Customer()
            {
                FirstName = firstName?.Text,
                LastName = lastName?.Text,
                PhoneNumber = phoneNumber?.Text,
                Email = email?.Text,
                TotalPurchasesAmount = totalPurchasesAmount
            };

            var addressesList = new List<Address>();

            foreach (RepeaterItem address in addresses.Items)
            {
                var newAddress = new Address
                {
                    AddressLine = ((TextBox) address.FindControl("addressLine"))?.Text,
                    AddressLine2 = ((TextBox) address.FindControl("addressLine2"))?.Text,
                    AddressType = (AddressTypes) Enum.Parse(typeof(AddressTypes),
                        ((DropDownList) address.FindControl("addressType")).SelectedItem.Value),
                    City = ((TextBox) address.FindControl("city"))?.Text,
                    PostalCode = ((TextBox) address.FindControl("postalCode"))?.Text,
                    State = ((TextBox) address.FindControl("state"))?.Text,
                    Country = ((DropDownList) address.FindControl("country"))?.SelectedItem.Value
                };

                if (customerIdReq != null)
                {
                    newAddress.CustomerId = Convert.ToInt32(customerIdReq);
                }

                var addressId = Convert.ToInt32(((TextBox) address.FindControl("addressId"))?.Text);
                if (addressId != 0)
                {
                    newAddress.AddressId = addressId;
                }

                addressesList.Add(newAddress);
            }

            var notesList = new List<Note>();

            foreach (RepeaterItem note in notes.Items)
            {
                var newNote = new Note
                {
                    NoteText = ((TextBox)note.FindControl("noteText"))?.Text,
                };

                if (customerIdReq != null)
                {
                    newNote.CustomerId = Convert.ToInt32(customerIdReq);
                }

                var noteId = Convert.ToInt32(((TextBox)note.FindControl("noteId"))?.Text);
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

        protected void Addresses_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                //var s = ((Repeater)source).Items;
                //addresses.DataSource = s;
                //if (s == null) addresses.DataSource = new List<Address>() {new Address()};
                //else addresses.DataSource = s.GetList().Add(new Address());
                //addresses.DataBind();
            }
        }
    }
}