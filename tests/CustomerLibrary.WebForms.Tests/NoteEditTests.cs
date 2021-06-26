using CustomerLibrary.BusinessLogic;
using Moq;
using Xunit;

namespace CustomerLibrary.WebForms.Tests
{
    public class NoteEditTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteEdit()
        {
            var noteEdit = new NoteEdit();

            Assert.NotNull(noteEdit);
        }

        [Fact]
        public void ShouldBeAbleToLoadNote()
        {
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note { CustomerId = 10 };
            noteServiceMock.Setup(s => s.Read(1)).Returns(note);

            var noteEdit = new NoteEdit(noteServiceMock.Object);
            noteEdit.LoadNote(1);

            Assert.Equal(10, noteEdit.CustomerId);
        }

        [Fact]
        public void ShouldCreateNote()
        {
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note();
            noteServiceMock.Setup(s => s.Create(note)).Returns(10);

            var noteEdit = new NoteEdit(noteServiceMock.Object);
            noteEdit.SaveNote(note);

            noteServiceMock.Verify(s => s.Create(note), Times.Exactly(1));
        }

        [Fact]
        public void ShouldUpdateNote()
        {
            var noteServiceMock = new Mock<IService<Note>>();
            var note = new Note { NoteId = 10 };
            noteServiceMock.Setup(s => s.Update(note));

            var noteEdit = new NoteEdit(noteServiceMock.Object);
            noteEdit.SaveNote(note);

            noteServiceMock.Verify(s => s.Update(note), Times.Exactly(1));
        }
    }
}
