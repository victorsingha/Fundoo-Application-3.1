using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        void AddNote(AddNote note);
        List<NoteResponse> GetAllNotes(int UserId);
        void UpdateNote(Note note);
        void DeleteNote(int noteId);
        void UpdateTitle(int noteId,string title);
    }
}
