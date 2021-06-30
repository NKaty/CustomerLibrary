using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebMVC.Controllers
{
    [Route("customers/page/{page:int=1}/{action=Index}/{id?}")]
    public class CustomersController : Controller
    {
        private readonly IMainService<Customer> _customerService;

        public int CustomersPerPage = 20;

        public CustomersController()
        {
            _customerService = new CustomerService();
        }

        public CustomersController(IMainService<Customer> customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public ActionResult Index(int page)
        {
            var offset = (page - 1) * CustomersPerPage;

            var (customers, count) = _customerService.ReadPage(offset, CustomersPerPage);

            ViewBag.LastPage = Convert.ToInt32(Math.Ceiling((double) count / CustomersPerPage));
            ViewBag.CurrentPage = page;

            return View(customers);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            var newCustomer = new Customer
            {
                Addresses = new List<Address> {new Address()},
                Notes = new List<Note> {new Note()}
            };

            return View(newCustomer);
        }

        // POST: Customers/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                var validationResult = CustomerValidator.Validate(customer);

                if (validationResult.Count != 0)
                {
                    foreach (var error in validationResult.Distinct())
                    {
                        ModelState.AddModelError("", error);
                    }

                    return View(customer);
                }

                _customerService.Create(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong.";

                return View(customer);
            }
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _customerService.Read(id);

            return View(customer);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int page, Customer customer)
        {
            try
            {
                var validationResult = CustomerValidator.Validate(customer);

                if (validationResult.Count != 0)
                {
                    foreach (var error in validationResult.Distinct())
                    {
                        ModelState.AddModelError("", error);
                    }

                    return View(customer);
                }

                _customerService.Update(customer);

                return RedirectToAction("Index", new {page});
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong.";

                return View(customer);
            }
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _customerService.Read(id);

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost]
        public ActionResult Delete(int page, int id, Customer customer)
        {
            try
            {
                _customerService.Delete(id);

                return RedirectToAction("Index", new {page});
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong.";

                return View(customer);
            }
        }
    }
}