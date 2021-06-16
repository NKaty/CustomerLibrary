using Xunit;

namespace CustomerLibrary.Tests
{
    public class NoteTests
    {
        [Fact]
        public void ShouldCreateNote()
        {
            Note note = new Note {NoteId = 1, CustomerId = 1, NoteText = "Note1"};

            Assert.Equal(1, note.NoteId);
            Assert.Equal(1, note.CustomerId);
            Assert.Equal("Note1", note.NoteText);
        }
    }
}