using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class NoteDeleteTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteDelete()
        {
            var noteDelete = new NoteDelete();

            Assert.NotNull(noteDelete);
        }

        [Fact]
        public void ShouldBeAbleToSetNoteToDelete()
        {
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note();
            noteServiceMock.Setup(s => s.Read(1)).Returns(note);

            var noteDelete = new NoteDelete(noteServiceMock.Object);
            noteDelete.SetNote(1);

            Assert.Equal(note, noteDelete.NoteToDelete);
        }

        [Fact]
        public void ShouldBeAbleToDeleteNote()
        {
            var noteServiceMock = new Mock<IService<Note>>();
            noteServiceMock.Setup(s => s.Delete(1));

            var noteDelete = new NoteDelete(noteServiceMock.Object);
            noteDelete.DeleteNote(1);

            noteServiceMock.Verify(s => s.Delete(1), Times.Exactly(1));
        }
    }
}
