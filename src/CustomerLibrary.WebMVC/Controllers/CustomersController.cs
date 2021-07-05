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

        // GET: Customers/page/1
        public ActionResult Index(int page)
        {
            var offset = (page - 1) * CustomersPerPage;

            var (customers, count) = _customerService.ReadPage(offset, CustomersPerPage);

            ViewBag.LastPage = Convert.ToInt32(Math.Ceiling((double) count / CustomersPerPage));
            ViewBag.CurrentPage = page;

            return View(customers);
        }

        // GET: Customers/page/1/Create
        public ActionResult Create()
        {
            var newCustomer = new Customer
            {
                Addresses = new List<Address> {new Address()},
                Notes = new List<Note> {new Note()}
            };

            TempData["AddressesStartLength"] = 1;
            TempData["NotesStartLength"] = 1;

            return View(newCustomer);
        }

        // POST: Customers/page/1/Create
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

                return RedirectToAction("Index", new {page = 1});
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Customers/page/1/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _customerService.Read(id);

            TempData["AddressesStartLength"] = customer.Addresses.Count;
            TempData["NotesStartLength"] = customer.Notes.Count;
            
            return View(customer);
        }

        // POST: Customers/page/1/Edit/5
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
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddAddress(int id, Customer customer)
        {
            var action = customer.CustomerId == 0 ? "Create" : "Edit";

            if (customer.Addresses == null)
            {
                customer.Addresses = new List<Address>();
            }

            customer.Addresses.Add(new Address {CustomerId = customer.CustomerId});

            return View(action, customer);
        }

        [HttpPost]
        public ActionResult DeleteAddress(int id, Customer customer)
        {
            var action = customer.CustomerId == 0 ? "Create" : "Edit";

            if (customer.Addresses.Count > (int)TempData.Peek("AddressesStartLength"))
            {
                customer.Addresses.RemoveAt(customer.Addresses.Count - 1);
            }
            
            return View(action, customer);
        }

        [HttpPost]
        public ActionResult AddNote(int id, Customer customer)
        {
            var action = customer.CustomerId == 0 ? "Create" : "Edit";

            if (customer.Notes == null)
            {
                customer.Notes = new List<Note>();
            }

            customer.Notes.Add(new Note { CustomerId = customer.CustomerId });

            return View(action, customer);
        }

        [HttpPost]
        public ActionResult DeleteNote(int id, Customer customer)
        {
            var action = customer.CustomerId == 0 ? "Create" : "Edit";

            if (customer.Notes.Count > (int)TempData.Peek("NotesStartLength"))
            {
                customer.Notes.RemoveAt(customer.Notes.Count - 1);
            }

            return View(action, customer);
        }

        // GET: Customers/page/1/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _customerService.Read(id);

            return View(customer);
        }

        // POST: Customers/page/1/Delete/5
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
                return View("Error");
            }
        }
    }
}