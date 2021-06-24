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
        void UpdateTitle(int noteId,string title);
        void UpdateBody(int noteId,string body);
        void UpdateReminder(int noteId, string reminder);
        void UpdateColor(int noteId, string color);
        void UpdateArchived(int noteId, bool isArchived);
        void UpdateTrash(int noteId,bool isTrash);
        void UpdatePin(int noteId, bool isPin);
    }
}
