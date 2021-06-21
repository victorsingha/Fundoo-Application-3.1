using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.DatabaseModel
{
    public class Label
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string Name { get; set; }
        
        // Navigation Properties
        public List<Note_Label> Note_Labels { get; set; }
        public List<User_Label> User_Labels { get; set; }
    }
}
