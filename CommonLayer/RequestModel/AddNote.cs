using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModel
{
    public class AddNote
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Reminder { get; set; }
        public string Color { get; set; }
        public bool isArchived { get; set; }
        public bool isTrash { get; set; }
        public bool isPin { get; set; }
        public int UserId { get; set; }
    }
}
