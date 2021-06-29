using System.Web.Mvc;
using CustomerLibrary.BusinessLogic;
using CustomerLibrary.WebMVC.Controllers;
using Moq;
using Xunit;

namespace CustomerLibrary.WebMVC.Tests.Controllers
{
    public class NotesControllerTests
    {
        [Fact]
        public void ShouldCreateNoteesController()
        {
            var controller = new NotesController();

            Assert.NotNull(controller);
        }

        [Fact]
        public void ShouldReturnListOfNotees()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var noteServiceMock = new Mock<IService<Note>>();
            var customer = new Customer() { CustomerId = 1, FirstName = "Bob", LastName = "Smith" };

            customerServiceMock.Setup(s => s.Read(1)).Returns(customer);
            var controller = new NotesController(customerServiceMock.Object, noteServiceMock.Object);

            var result = controller.Index(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal("Bob", result.ViewData["FirstName"]);
            Assert.Equal("Smith", result.ViewData["LastName"]);
        }

        [Fact]
        public void ShouldReturnViewToCreateNote()
        {
            var controller = new NotesController();

            var result = controller.Create(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(1, (result.ViewData.Model as Note)?.CustomerId);
        }

        [Fact]
        public void ShouldCreateNote()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note();

            var controller = new NotesController(customerServiceMock.Object, noteServiceMock.Object);

            var result = controller.Create(note) as RedirectToRouteResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnViewToEditNote()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note() { CustomerId = 10 };

            noteServiceMock.Setup(s => s.Read(1)).Returns(note);
            var controller = new NotesController(customerServiceMock.Object, noteServiceMock.Object);

            var result = controller.Edit(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(10, (result.ViewData.Model as Note)?.CustomerId);
        }

        [Fact]
        public void ShouldEditNote()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note();

            var controller = new NotesController(customerServiceMock.Object, noteServiceMock.Object);

            var result = controller.Edit(note) as RedirectToRouteResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void ShouldReturnViewToDeleteNote()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note() { CustomerId = 10 };

            noteServiceMock.Setup(s => s.Read(1)).Returns(note);
            var controller = new NotesController(customerServiceMock.Object, noteServiceMock.Object);

            var result = controller.Delete(1) as ViewResult;

            Assert.NotNull(result);
            Assert.Equal(10, (result.ViewData.Model as Note)?.CustomerId);
        }

        [Fact]
        public void ShouldDeleteNote()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note { NoteId = 1 };

            var controller = new NotesController(customerServiceMock.Object, noteServiceMock.Object);

            var result = controller.Delete(1, note) as RedirectToRouteResult;

            Assert.NotNull(result);
        }
    }
}
