using System.Collections.Generic;
using System.Web.Mvc;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebMVC.Controllers
{
    [Route("customers/{customerId}/addresses/{action=Index}/{addressId?}")]
    public class AddressesController : Controller
    {
        private readonly IMainService<Customer> _customerService;
        private readonly IService<Address> _addressService;

        public AddressesController()
        {
            _customerService = new CustomerService();
            _addressService = new AddressService();
        }

        public AddressesController(IMainService<Customer> customerService, IService<Address> addressService)
        {
            _customerService = customerService;
            _addressService = addressService;
        }

        // GET: Customers/1/Addresses
        [HttpGet]
        public ActionResult Index(int customerId)
        {
            var customer = _customerService.Read(customerId);
            ViewData.Add(new KeyValuePair<string, object>("FirstName", customer.FirstName));
            ViewData.Add(new KeyValuePair<string, object>("LastName", customer.LastName));

            return View(customer.Addresses);
        }

        // GET: Customers/1/Addresses/Create
        [HttpGet]
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
                _addressService.Create(address);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(address);
            }
        }

        // GET: Customers/1/Addresses/Edit/5
        [HttpGet]
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
                _addressService.Update(address);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(address);
            }
        }

        // GET: Customers/1/Addresses/Delete/5
        [HttpGet]
        public ActionResult Delete(int addressId)
        {
            var address = _addressService.Read(addressId);

            return View(address);
        }

        // POST: Customers/1/Addresses/Delete/5
        [HttpPost]
        public ActionResult Delete(int addressId, Address address)
        {
            try
            {
                _addressService.Delete(addressId);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(address);
            }
        }
    }
}
