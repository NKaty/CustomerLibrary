using CustomerLibrary.Data;

namespace CustomerLibrary.BusinessLogic
{
    public class NoteProvider
    {
        private readonly NoteRepository _noteRepository = new();

        public int Create(Note note)
        {
            return _noteRepository.Create(note);
        }

        public Note Read(int noteId)
        {
            return _noteRepository.Read(noteId);
        }

        public void Update(Note note)
        {
            _noteRepository.Update(note);
        }

        public void Delete(int noteId)
        {
            _noteRepository.Delete(noteId);
        }
    }
}