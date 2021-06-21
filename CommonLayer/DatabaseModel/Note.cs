using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NotesId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Reminder { get; set; }
        public string Color { get; set; }
        public bool isArchived{ get; set; }
        public bool isTrash { get; set; }
        public bool isPin { get; set; }

        // Navigation Properties
        public int UserId { get; set; }
        public User user { get; set; }

        public List<Note_Label> Note_Labels { get; set; }
    }
}
