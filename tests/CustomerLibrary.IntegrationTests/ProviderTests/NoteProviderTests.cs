using CustomerLibrary.BusinessLogic;
using CustomerLibrary.Data;
using CustomerLibrary.IntegrationTests.RepositoryTests;
using Xunit;

namespace CustomerLibrary.IntegrationTests.ProviderTests
{
    public class NoteProviderTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteProvider()
        {
            var noteProvider = new NoteProvider();
            Assert.NotNull(noteProvider);
        }

        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            var fixture = new NoteProviderFixture();
            var mockNoteId = fixture.CreateMockNote();
            Assert.NotEqual(0, mockNoteId);
        }

        [Fact]
        public void ShouldBeAbleToReadNote()
        {
            var noteProvider = new NoteProvider();
            var fixture = new NoteProviderFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteProvider.Read(noteId);

            Assert.NotNull(createdNote);
            Assert.Equal(fixture.MockNote.NoteId, createdNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, createdNote.CustomerId);
            Assert.Equal(fixture.MockNote.NoteText, createdNote.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToUpdateNote()
        {
            var noteProvider = new NoteProvider();
            var fixture = new NoteProviderFixture();
            var noteId = fixture.CreateMockNote();

            fixture.MockNote.NoteText = "Test";
            noteProvider.Update(fixture.MockNote);
            var updatedNote = noteProvider.Read(noteId);

            Assert.NotNull(updatedNote);
            Assert.Equal(fixture.MockNote.NoteId, updatedNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, updatedNote.CustomerId);
            Assert.Equal("Test", updatedNote.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        {
            var noteProvider = new NoteProvider();
            var fixture = new NoteProviderFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteProvider.Read(noteId);

            Assert.NotNull(createdNote);

            noteProvider.Delete(noteId);
            var deletedNote = noteProvider.Read(noteId);

            Assert.Null(deletedNote);
        }
    }

    public class NoteProviderFixture
    {
        public Note MockNote { get; set; } = new Note
        {
            NoteText = "Note1"
        };

        public int CreateMockNote()
        {
            var noteProvider = new NoteProvider();
            var noteRepository = new NoteRepository();

            noteRepository.DeleteAll();

            var customerFixture = new CustomerRepositoryFixture();
            var customerId = customerFixture.CreateMockCustomer();

            MockNote.CustomerId = customerId;
            var newNoteId = noteProvider.Create(MockNote);
            return newNoteId;
        }
    }
}