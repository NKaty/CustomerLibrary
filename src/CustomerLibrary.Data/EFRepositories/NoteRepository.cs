using CustomerLibrary.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace CustomerLibrary.Data.EFRepositories
{
    public class NoteRepository : IDependentRepository<Note>
    {
        private readonly CustomerLibraryContext _context;

        public NoteRepository()
        {
            _context = new CustomerLibraryContext();
        }

        public NoteRepository(CustomerLibraryContext context)
        {
            _context = context;
        }

        public int Create(Note note)
        {
            var newNote = _context.Notes.Add(note);

            _context.SaveChanges();

            return newNote.NoteId;
        }

        public Note Read(int noteId)
        {
            return _context.Notes.Find(noteId);
        }

        public List<Note> ReadByCustomerId(int customerId)
        {
            return _context.Notes.Where(n => n.CustomerId == customerId).ToList();
        }

        public int CountByCustomerId(int customerId)
        {
            return _context.Notes.Count(a => a.CustomerId == customerId);
        }

        public void Update(Note note)
        {
            var dbNote = _context.Notes.Find(note.NoteId);

            if (dbNote != null)
            {
                _context.Entry(dbNote).CurrentValues.SetValues(note);

                _context.SaveChanges();
            }
        }

        public void Delete(int noteId)
        {
            var note = _context.Notes.Find(noteId);

            if (note != null)
            {
                _context.Notes.Remove(note);

                _context.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            var notes = _context.Notes.ToList();

            foreach (var note in notes)
            {
                _context.Notes.Remove(note);
            }

            _context.SaveChanges();
        }
    }
}
