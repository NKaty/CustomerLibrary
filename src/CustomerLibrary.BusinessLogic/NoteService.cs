using CustomerLibrary.BusinessLogic.Common;
using CustomerLibrary.Data;
using System.Collections.Generic;

namespace CustomerLibrary.BusinessLogic
{
    public class NoteService : IService<Note>
    {
        private readonly IDependentRepository<Note> _noteRepository;

        public NoteService()
        {
            _noteRepository = new NoteRepository();
        }

        public NoteService(IDependentRepository<Note> noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public int Create(Note note)
        {
            var noteId = _noteRepository.Create(note);

            if (noteId == 0)
            {
                throw new NotCreatedException("Note was not created.");
            }

            return noteId;
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