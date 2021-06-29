using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CustomerLibrary
{
    [Serializable]
    public class Note
    {
        public int NoteId { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("Note")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Note is required.")]
        [MaxLength(100, ErrorMessage = "Note must be max 500 chars long.")]
        public string NoteText { get; set; }
    }
}