using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CustomerLibrary.WebMVC.Controllers
{
    [Route("customers/{customerId}/addresses/{action=Index}/{addressId?}")]
    public class AddressesController : Controller
    {
        private readonly IMainService<Customer> _customerService;
        private readonly IDependentService<Address> _addressService;

        public AddressesController()
        {
            _customerService = new CustomerService();
            _addressService = new AddressService();
        }

        public AddressesController(IMainService<Customer> customerService, IDependentService<Address> addressService)
        {
            _customerService = customerService;
            _addressService = addressService;
        }

        // GET: Customers/1/Addresses
        public ActionResult Index(int customerId)
        {
            var customer = _customerService.Read(customerId);
            ViewData.Add(new KeyValuePair<string, object>("FirstName", customer.FirstName));
            ViewData.Add(new KeyValuePair<string, object>("LastName", customer.LastName));

            return View(customer.Addresses);
        }

        // GET: Customers/1/Addresses/Create
        public ActionResult Create(int customerId)
        {
            var newAddress = new Address {CustomerId = customerId};

            return View(newAddress);
        }

        // POST: Customers/1/Addresses/Create
        [HttpPost]
        public ActionResult Create(Address address)
        {
            try
            {
                var validationResult = AddressValidator.Validate(address);

                if (validationResult.Count != 0)
                {
                    foreach (var error in validationResult.Distinct())
                    {
                        ModelState.AddModelError("", error);
                    }

                    return View(address);
                }

                _addressService.Create(address);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Customers/1/Addresses/Edit/5
        public ActionResult Edit(int addressId)
        {
            var address = _addressService.Read(addressId);

            return View(address);
        }

        // POST: Customers/1/Addresses/Edit/5
        [HttpPost]
        public ActionResult Edit(Address address)
        {
            try
            {
                var validationResult = AddressValidator.Validate(address);

                if (validationResult.Count != 0)
                {
                    foreach (var error in validationResult.Distinct())
                    {
                        ModelState.AddModelError("", error);
                    }

                    return View(address);
                }

                _addressService.Update(address);

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: Customers/1/Addresses/Delete/5
        public ActionResult Delete(int addressId)
        {
            var address = _addressService.Read(addressId);

            return View(address);
        }

        // POST: Customers/1/Addresses/Delete/5
        [HttpPost]
        public ActionResult Delete(Address address)
        {
            try
            {
                _addressService.Delete(address);

                return RedirectToAction("Index");
            }
            catch (NotDeletedException exception)
            {
                ViewBag.ErrorMessage = exception.Message;

                return View(address);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
