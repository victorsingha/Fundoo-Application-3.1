using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.ResponseModel
{
    public class NoteResponse
    {
        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Reminder { get; set; }
        public string Color { get; set; }
        public bool isArchived { get; set; }
        public bool isTrash { get; set; }
        public bool isPin { get; set; }

        // Navigation Properties
        public int UserId { get; set; }
    }
}
