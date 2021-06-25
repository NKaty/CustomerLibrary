using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CustomerLibrary.BusinessLogic;

namespace CustomerLibrary.WebForms
{
    public partial class NoteDelete : System.Web.UI.Page
    {
        private readonly IService<Note> _noteService;

        public Note NoteToDelete { get; set; }

        public NoteDelete()
        {
            _noteService = new NoteService();
        }

        public NoteDelete(IService<Note> noteService)
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

        public void DeleteNote(int customerId)
        {
            _noteService.Delete(customerId);
        }

        public void OnDeleteClick(object sender, EventArgs e)
        {
            var noteId = GetNoteId();

            if (noteId != 0)
            {
                DeleteNote(noteId);
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