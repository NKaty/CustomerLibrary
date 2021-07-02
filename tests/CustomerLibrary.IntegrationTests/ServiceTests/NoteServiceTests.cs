using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data.Repositories;
using CustomerLibrary.IntegrationTests.RepositoryTests;
using Xunit;

namespace CustomerLibrary.IntegrationTests.ServiceTests
{
    public class NoteServiceTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            var fixture = new  NoteServiceFixture();
            var mockNoteId = fixture.CreateMockNote();
            Assert.NotEqual(0, mockNoteId);
        }

        [Fact]
        public void ShouldBeAbleToReadNote()
        {
            var noteService = new NoteService();
            var fixture = new  NoteServiceFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteService.Read(noteId);

            Assert.NotNull(createdNote);
            Assert.Equal(fixture.MockNote.NoteId, createdNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, createdNote.CustomerId);
            Assert.Equal(fixture.MockNote.NoteText, createdNote.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToUpdateNote()
        {
            var noteService = new NoteService();
            var fixture = new  NoteServiceFixture();
            var noteId = fixture.CreateMockNote();

            fixture.MockNote.NoteText = "Test";
            noteService.Update(fixture.MockNote);
            var updatedNote = noteService.Read(noteId);

            Assert.NotNull(updatedNote);
            Assert.Equal(fixture.MockNote.NoteId, updatedNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, updatedNote.CustomerId);
            Assert.Equal("Test", updatedNote.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        {
            var noteService = new NoteService();
            var fixture = new  NoteServiceFixture();
            var noteId = fixture.CreateTwoMockNotes();
            var createdNote = noteService.Read(noteId);

            Assert.NotNull(createdNote);

            noteService.Delete(createdNote);
            var deletedNote = noteService.Read(noteId);

            Assert.Null(deletedNote);
        }


        [Fact]
        public void ShouldNotDeleteTheOnlyNote()
        {
            var noteService = new NoteService();
            var fixture = new NoteServiceFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteService.Read(noteId);

            Assert.NotNull(createdNote);

            Assert.Throws<NotDeletedException>(() => noteService.Delete(createdNote));
        }
    }

    public class  NoteServiceFixture
    {
        public Note MockNote { get; set; } = new Note
        {
            NoteText = "Note1"
        };

        public int CreateMockNote()
        {
            var noteService = new NoteService();
            var noteRepository = new NoteRepository();

            noteRepository.DeleteAll();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            MockNote.CustomerId = customerId;
            var newNoteId = noteService.Create(MockNote);
            return newNoteId;
        }

        public int CreateTwoMockNotes()
        {
            var noteService = new NoteService();
            var noteRepository = new NoteRepository();

            noteRepository.DeleteAll();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            MockNote.CustomerId = customerId;
            noteService.Create(MockNote);

            MockNote.CustomerId = customerId;
            var newNoteId = noteService.Create(MockNote);

            return newNoteId;
        }
    }
}