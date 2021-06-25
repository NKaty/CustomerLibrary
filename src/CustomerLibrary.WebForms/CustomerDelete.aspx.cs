using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class CustomerDelete : System.Web.UI.Page
    {
        private readonly IMainService<Customer> _customerService;

        public Customer CustomerToDelete { get; set; }

        public CustomerDelete()
        {
            _customerService = new CustomerService();
        }

        public CustomerDelete(IMainService<Customer> customerService)
        {
            _customerService = customerService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var customerId = GetCustomerId();

            SetCustomer(customerId);
        }

        private int GetCustomerId()
        {
            int.TryParse(Request.QueryString["customerId"], out var customerIdReq);

            return customerIdReq;
        }

        public void SetCustomer(int customerId)
        {
            CustomerToDelete = customerId == 0 ? null : _customerService.Read(customerId);
        }

        public void DeleteCustomer(int customerId)
        {
            _customerService.Delete(customerId);
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            var customerId = GetCustomerId();
            if (customerId != 0)
            {
                DeleteCustomer(customerId);
            }

            Response?.Redirect("CustomerList");
        }
    }
}