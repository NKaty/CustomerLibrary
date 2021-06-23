using System;
using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class CustomerList : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public List<Customer> Customers { get; set; }

        public CustomerList()
        {
            _customerService = new CustomerService();
        }

        public CustomerList(IMainService<Customer> customerService)
        {
            _customerService = customerService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCustomersFromDatabase();
        }

        public void LoadCustomersFromDatabase()
        {
            Customers = _customerService.ReadAll();
        }
    }
}