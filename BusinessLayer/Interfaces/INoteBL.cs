using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INoteBL
    {
        void AddNote(AddNote note);
        List<NoteResponse> GetAllNotes(int UserId);
        void UpdateNote(Note note);
        void DeleteNote(int noteId);
    }
}
