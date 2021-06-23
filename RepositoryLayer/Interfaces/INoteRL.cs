using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        void AddNote(AddNote note);
        List<Note> GetAllNotes(int UserId);
        void UpdateNote(Note note);
        void DeleteNote(int noteId);
    }
}
