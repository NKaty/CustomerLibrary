using System;
using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class CustomerList : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public int CustomersPerPage = 20;

        public int LastPage { get; set; } = 0;

        public int CurrentPage { get; set; } = 0;

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
            int.TryParse(Request.QueryString["page"], out var pageReq);

            var offset = SetPagination(pageReq);

            LoadCustomersFromDatabase(offset, CustomersPerPage);
        }

        public int SetPagination(int page)
        {
            if (LastPage == 0)
            {
                LastPage = Convert.ToInt32(Math.Ceiling((double)_customerService.Count() / CustomersPerPage));
            }

            int offset;

            if (page == 0)
            {
                offset = 0;
                CurrentPage = 1;
            }
            else
            {
                offset = (page - 1) * CustomersPerPage;
                CurrentPage = page;
            }

            return offset;
        }

        public void LoadCustomersFromDatabase(int offset, int limit)
        {
            Customers = _customerService.ReadPage(offset, limit);
        }
    }
}