using System.Collections.Generic;
using System.Web.Mvc;
using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;

namespace CustomerLibrary.WebMVC.Controllers
{
    [Route("customers/{customerId}/notes/{action=Index}/{noteId?}")]
    public class NotesController : Controller
    {
        private readonly IMainService<Customer> _customerService;
        private readonly IDependentService<Note> _noteService;

        public NotesController()
        {
            _customerService = new CustomerService();
            _noteService = new NoteService();
        }

        public NotesController(IMainService<Customer> customerService, IDependentService<Note> noteService)
        {
            _customerService = customerService;
            _noteService = noteService;
        }
        // GET:  Customers/1/Notes
        public ActionResult Index(int customerId)
        {

            var customer = _customerService.Read(customerId);
            ViewData.Add(new KeyValuePair<string, object>("FirstName", customer.FirstName));
            ViewData.Add(new KeyValuePair<string, object>("LastName", customer.LastName));

            return View(customer.Notes);
        }

        // GET:  Customers/1/Notes/Create
        public ActionResult Create(int customerId)
        {
            var newNote = new Note { CustomerId = customerId };

            return View(newNote);
        }

        // POST:  Customers/1/Notes/Create
        [HttpPost]
        public ActionResult Create(Note note)
        {
            try
            {
                _noteService.Create(note);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong.";

                return View(note);
            }
        }

        // GET:  Customers/1/Notes/Edit/5
        public ActionResult Edit(int noteId)
        {
            var note = _noteService.Read(noteId);

            return View(note);
        }

        // POST:  Customers/1/Notes/Edit/5
        [HttpPost]
        public ActionResult Edit(Note note)
        {
            try
            {
                _noteService.Update(note);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong.";

                return View(note);
            }
        }

        // GET:  Customers/1/Notes/Delete/5
        public ActionResult Delete(int noteId)
        {
            var note = _noteService.Read(noteId);

            return View(note);
        }

        // POST:  Customers/1/Notes/Delete/5
        [HttpPost]
        public ActionResult Delete(Note note)
        {
            try
            {
                _noteService.Delete(note);

                return RedirectToAction("Index");
            }
            catch (NotDeletedException exception)
            {
                ViewBag.ErrorMessage = exception.Message;

                return View(note);
            }
            catch
            {
                ViewBag.ErrorMessage = "Something went wrong.";

                return View(note);
            }
        }
    }
}
