using CustomerLibrary.Data;
using Xunit;

namespace CustomerLibrary.IntegrationTests
{
    public class NoteRepositoryTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteRepository()
        {
            var noteRepository = new NoteRepository();
            Assert.NotNull(noteRepository);
        }

        [Fact]
        public void ShouldBeAbleToCreateNote()
        {
            var fixture = new NoteRepositoryFixture();
            var mockNoteId = fixture.CreateMockNote();
            Assert.NotEqual(0, mockNoteId);
        }

        [Fact]
        public void ShouldBeAbleToReadNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteRepository.Read(noteId);

            Assert.NotNull(createdNote);
            Assert.Equal(fixture.MockNote.NoteId, createdNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, createdNote.CustomerId);
            Assert.Equal(fixture.MockNote.NoteText, createdNote.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToUpdateNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var noteId = fixture.CreateMockNote();

            fixture.MockNote.NoteText = "Test";
            noteRepository.Update(fixture.MockNote);
            var updatedNote = noteRepository.Read(noteId);

            Assert.NotNull(updatedNote);
            Assert.Equal(fixture.MockNote.NoteId, updatedNote.NoteId);
            Assert.Equal(fixture.MockNote.CustomerId, updatedNote.CustomerId);
            Assert.Equal("Test", updatedNote.NoteText);
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        {
            var noteRepository = new NoteRepository();
            var fixture = new NoteRepositoryFixture();
            var noteId = fixture.CreateMockNote();
            var createdNote = noteRepository.Read(noteId);

            Assert.NotNull(createdNote);

            noteRepository.Delete(noteId);
            var deletedNote = noteRepository.Read(noteId);

            Assert.Null(deletedNote);
        }
    }

    public class NoteRepositoryFixture
    {
        public Note MockNote { get; set; } = new Note
        {
            NoteText = "Note1"
        };

        public int CreateMockNote(int customerId = 0)
        {
            var noteRepository = new NoteRepository();
            noteRepository.DeleteAll();

            if (customerId == 0)
            {
                var customerFixture = new CustomerRepositoryFixture();
                customerId = customerFixture.CreateMockCustomer();
            }

            MockNote.CustomerId = customerId;
            var newNoteId = noteRepository.Create(MockNote);
            return newNoteId;
        }
    }
}
