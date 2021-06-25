using System;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class AddressList : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public Customer Customer { get; set; }

        public AddressList()
        {
            _customerService = new CustomerService();
        }

        public AddressList(IMainService<Customer> customerService)
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