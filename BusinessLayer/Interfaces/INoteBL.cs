using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        void AddNote(AddNote note);
        List<Note> GetAllNotes(int UserId);
    }
}
