using CustomerLibrary.BusinessLogic;
using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data;
using Moq;
using Xunit;

namespace CustomerLibrary.Tests.ServicesTests
{
    public class NoteServiceTests
    {
        [Fact]
        public void ShouldBeAbleToCreateNoteService()
        {
            var noteService = new NoteService();
            Assert.NotNull(noteService);
        }

        [Fact]
        public void ShouldCallRepositoryCreate()
        {
            var fixture = new NoteServiceFixture();
            fixture.NoteRepositoryMock.Setup(r => r.Create(fixture.MockNote)).Returns(1);
            var service = fixture.CreateService();

            var noteId = service.Create(fixture.MockNote);
            Assert.Equal(1, noteId);

            fixture.NoteRepositoryMock.Verify(r => r.Create(fixture.MockNote), Times.Exactly(1));
        }

        [Fact]
        public void ShouldThrowNotCreatedException()
        {
            var fixture = new NoteServiceFixture();
            fixture.NoteRepositoryMock.Setup(r => r.Create(fixture.MockNote)).Returns(0);
            var service = fixture.CreateService();

            Assert.Throws<NotCreatedException>(() => service.Create(fixture.MockNote));

            fixture.NoteRepositoryMock.Verify(r => r.Create(fixture.MockNote), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCallRepositoryRead()
        {
            var fixture = new NoteServiceFixture();
            fixture.NoteRepositoryMock.Setup(r => r.Read(1)).Returns(fixture.MockNote);
            var service = fixture.CreateService();

            var note = service.Read(1);
            Assert.Equal(fixture.MockNote, note);

            fixture.NoteRepositoryMock.Verify(r => r.Read(1), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCallRepositoryUpdate()
        {
            var fixture = new NoteServiceFixture();
            fixture.NoteRepositoryMock.Setup(r => r.Update(fixture.MockNote));
            var service = fixture.CreateService();

            service.Update(fixture.MockNote);

            fixture.NoteRepositoryMock.Verify(r => r.Update(fixture.MockNote), Times.Exactly(1));
        }

        [Fact]
        public void ShouldCallRepositoryDelete()
        {
            var fixture = new NoteServiceFixture();
            fixture.NoteRepositoryMock.Setup(r => r.Delete(1));
            var service = fixture.CreateService();

            service.Delete(1);

            fixture.NoteRepositoryMock.Verify(r => r.Delete(1), Times.Exactly(1));
        }

        public class NoteServiceFixture
        {
            public Mock<IDependentRepository<Note>> NoteRepositoryMock { get; set; }

            public Note MockNote { get; set; } = new Note
            {
                NoteId = 1,
                CustomerId = 1,
                NoteText = "Note1"
            };

            public NoteServiceFixture()
            {
                NoteRepositoryMock = new Mock<IDependentRepository<Note>>();
            }

            public NoteService CreateService()
            {
                return new NoteService(NoteRepositoryMock.Object);
            }
        }
    }
}