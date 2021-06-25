using System;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class NoteList : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public Customer Customer { get; set; }

        public NoteList()
        {
            _customerService = new CustomerService();
        }

        public NoteList(IMainService<Customer> customerService)
        {
            _customerService = customerService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
            {
                LoadCustomerFromDatabase(customerIdReq);
            }
        }

        public void LoadCustomerFromDatabase(int customerId)
        {
            Customer = _customerService.Read(customerId);
        }
    }
}