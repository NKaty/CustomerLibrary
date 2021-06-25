using System.Collections.Generic;
using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class NoteListTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteList()
        {
            var noteList = new NoteList();

            Assert.NotNull(noteList);
        }

        [Fact]
        public void ShouldBeAbleToGetCustomerWithNotes()
        {
            var customerServiceMock = new Mock<IMainService<Customer>>();
            customerServiceMock.Setup(s => s.Read(1))
                .Returns(() => new Customer() {Notes = new List<Note> { new Note() } });

            var noteList = new NoteList(customerServiceMock.Object);
            noteList.LoadCustomerFromDatabase(1);

            Assert.NotNull(noteList.Customer);
            Assert.Single(noteList.Customer.Notes);
        }
    }
}
