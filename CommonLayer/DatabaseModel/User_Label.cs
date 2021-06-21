using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class User_Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_Label_Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int LabelId { get; set; }
        public Label Label { get; set; }
    }
}
