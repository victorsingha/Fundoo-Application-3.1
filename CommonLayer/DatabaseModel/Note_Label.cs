using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class Note_Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Note_Label_Id { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }
    }
}
