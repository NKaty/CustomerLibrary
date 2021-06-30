using CustomerLibrary.BusinessLogic;
using System;

namespace CustomerLibrary.WebForms
{
    public partial class NoteDelete : System.Web.UI.Page
    {
        private readonly IDependentService<Note> _noteService;

        public Note NoteToDelete { get; set; }

        public NoteDelete()
        {
            _noteService = new NoteService();
        }

        public NoteDelete(IDependentService<Note> noteService)
        {
            _noteService = noteService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var noteId = GetNoteId();

            SetNote(noteId);
        }

        private int GetNoteId()
        {
            int.TryParse(Request.QueryString["noteId"], out var noteIdReq);

            return noteIdReq;
        }

        public void SetNote(int noteId)
        {
            NoteToDelete = noteId == 0 ? null : _noteService.Read(noteId);
        }

        public void DeleteNote(Note note)
        {
            _noteService.Delete(note);
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            var noteId = GetNoteId();

            if (noteId != 0)
            {
                SetNote(noteId);
                DeleteNote(NoteToDelete);
            }

            if (int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
            {
                Response?.Redirect($"NoteList?customerId={customerIdReq}");
            }
            else
            {
                Response?.Redirect("CustomerList");
            }

        }
    }
}