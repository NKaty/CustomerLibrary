using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class CustomerEdit : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public Customer Customer { get; set; }

        public CustomerEdit()
        {
            _customerService = new CustomerService();
        }

        public CustomerEdit(IMainService<Customer> customerService)
        {
            _customerService = customerService;
        }

        protected override void LoadViewState(object savedState)
        {
            base.LoadViewState(savedState);

            Customer = ViewState["CustomerObject"] as Customer;

            if (Customer != null)
            {
                addresses.DataSource = Customer.Addresses;
                addresses.DataBind();

                notes.DataSource = Customer.Notes;
                notes.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int.TryParse(Request.QueryString["customerId"], out var customerIdReq);

                LoadCustomer(customerIdReq);
            }
            else
            {
                ReadCustomerForm();
            }
        }

        private void ReadCustomerForm()
        {
            int.TryParse(Request.QueryString["customerId"], out var customerIdReq);

            decimal.TryParse(amount?.Text, out var totalPurchasesAmount);

            Customer.CustomerId = customerIdReq;
            Customer.FirstName = firstName?.Text;
            Customer.LastName = lastName?.Text;
            Customer.PhoneNumber = phoneNumber?.Text;
            Customer.Email = email?.Text;
            Customer.TotalPurchasesAmount = totalPurchasesAmount;


            for (var index = 0; index < addresses.Items.Count; index++)
            {
                var item = addresses.Items[index];
                var address = Customer.Addresses[index];

                int.TryParse(((HiddenField) item.FindControl("addressId"))?.Value, out var addressId);

                address.AddressId = addressId;
                address.CustomerId = customerIdReq;
                address.AddressLine = ((TextBox) item.FindControl("addressLine"))?.Text;
                address.AddressLine2 = ((TextBox) item.FindControl("addressLine2"))?.Text;
                address.AddressType = (AddressTypes) Enum.Parse(typeof(AddressTypes),
                    ((DropDownList) item.FindControl("addressType")).SelectedItem.Value);
                address.City = ((TextBox) item.FindControl("city"))?.Text;
                address.PostalCode = ((TextBox) item.FindControl("postalCode"))?.Text;
                address.State = ((TextBox) item.FindControl("state"))?.Text;
                address.Country = ((DropDownList) item.FindControl("country"))?.SelectedItem.Value;
            }

            for (var index = 0; index < notes.Items.Count; index++)
            {
                var item = notes.Items[index];
                var note = Customer.Notes[index];

                int.TryParse(((HiddenField) item.FindControl("noteId"))?.Value, out var noteId);

                note.NoteId = noteId;
                note.CustomerId = customerIdReq;
                note.NoteText = ((TextBox) item.FindControl("noteText"))?.Text;
            }
        }

        public void LoadCustomer(int customerIdReq)
        {
            if (customerIdReq != 0)
            {
                Customer = _customerService.Read(customerIdReq);

                firstName.Text = Customer.FirstName;
                lastName.Text = Customer.LastName;
                email.Text = Customer.Email;
                phoneNumber.Text = Customer.PhoneNumber;
                amount.Text = Customer.TotalPurchasesAmount.ToString();
            }
            else
            {
                Customer = new Customer
                {
                    Addresses = new List<Address> {new Address()},
                    Notes = new List<Note> {new Note()}
                };
            }

            addresses.DataSource = Customer.Addresses;
            notes.DataSource = Customer.Notes;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            addresses.DataBind();
            notes.DataBind();

            DataBind();

            ViewState["CustomerObject"] = Customer;
        }

        public void SaveCustomer()
        {
            if (Customer.CustomerId == 0)
            {
                _customerService.Create(Customer);
            }
            else
            {
                _customerService.Update(Customer);
            }
        }

        protected void OnSaveClick(object sender, EventArgs e)
        {
            if (!decimal.TryParse(amount?.Text, out _))
            {
                amountError.Text = "Total purchases amount must be a number.";
                return;
            }

            SaveCustomer();

            Response?.Redirect("CustomerList");
        }

        public void AddAddress(object sender, EventArgs e)
        {
            Customer?.Addresses.Add(new Address());
        }

        public void DeleteAddress(object sender, EventArgs e)
        {
            if (Customer.Addresses.Count > 1)
            {
                Customer?.Addresses.RemoveAt(Customer.Addresses.Count - 1);
            }
        }

        public void AddNote(object sender, EventArgs e)
        {
            Customer?.Notes.Add(new Note());
        }

        public void DeleteNote(object sender, EventArgs e)
        {
            if (Customer.Notes.Count > 1)
            {
                Customer?.Notes.RemoveAt(Customer.Notes.Count - 1);
            }
        }
    }
}