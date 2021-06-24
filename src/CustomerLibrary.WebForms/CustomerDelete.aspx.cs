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

            if (!int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
            {
                CustomerToDelete = null;
            }

            CustomerToDelete = _customerService.Read(customerIdReq);
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            if (int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
            {
                _customerService.Delete(customerIdReq);
            }

            Response?.Redirect("CustomerList");
        }
    }
}