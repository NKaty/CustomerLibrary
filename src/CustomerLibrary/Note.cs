using System;

namespace CustomerLibrary
{
    [Serializable]
    public class Note
    {
        public int NoteId { get; set; }

        public int CustomerId { get; set; }

        public string NoteText { get; set; }
    }
}