using System;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class NoteEdit : System.Web.UI.Page
    {
        private readonly IService<Note> _noteService;

        public int CustomerId { get; set; }

        public NoteEdit()
        {
            _noteService = new NoteService();
        }

        public NoteEdit(IService<Note> noteService)
        {
            _noteService = noteService;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (int.TryParse(Request.QueryString["noteId"], out var noteIdReq))
            {
                var note = LoadNote(noteIdReq);

                customerId.Value = note.CustomerId.ToString();
                noteId.Value = note.NoteId.ToString();
                noteText.Text = note.NoteText;
            }
            else
            {
                if (int.TryParse(Request.QueryString["customerId"], out var customerIdReq))
                {
                    customerId.Value = customerIdReq.ToString();
                    CustomerId = customerIdReq;
                }
            }
        }

        public Note LoadNote(int noteIdReq)
        {
            var note = _noteService.Read(noteIdReq);
            CustomerId = note.CustomerId;

            return note;
        }

        public void SaveNote(Note note)
        {
            if (note.NoteId != 0)
            {
                _noteService.Update(note);
            }
            else
            {
                _noteService.Create(note);
            }
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            if (int.TryParse(customerId?.Value, out var currentCustomerId))
            {
                var note = new Note
                {
                    CustomerId = currentCustomerId,
                    NoteText = noteText?.Text,
                };

                if (int.TryParse(noteId?.Value, out var currentNoteId))
                {
                    note.NoteId = currentNoteId;
                }

                SaveNote(note);

                Response?.Redirect($"NoteList?customerId={currentCustomerId}");
            }

            Response?.Redirect("CustomerList");
        }
    }
}