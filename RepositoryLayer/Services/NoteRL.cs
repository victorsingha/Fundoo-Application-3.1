using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL: INoteRL
    {
        FundooContext _fundooContext;
        public NoteRL(FundooContext fundooContext)
        {
            _fundooContext = fundooContext;
        }

        public void AddNote(AddNote note)
        {
            try
            {
                var user = _fundooContext.Users.FirstOrDefault(e => e.UserId == note.UserId);
                if(user != null)
                {
                    Note notedb = new Note();
                    notedb.Title = note.Title;
                    notedb.Body = note.Body;
                    notedb.Reminder = note.Reminder;
                    notedb.Color = note.Color;
                    notedb.isArchived = note.isArchived;
                    notedb.isTrash = note.isTrash;
                    notedb.isPin = note.isPin;
                    notedb.UserId = note.UserId;
                    //notedb.user = user;
                    //notedb.Note_Labels = null;
                    _fundooContext.Notes.Add(notedb);
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("User ID doesn't Exist.");
                }
                
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<Note> GetAllNotes(int UserId)
        {
            try
            {
                var list = _fundooContext.Notes.Where(e => e.UserId == UserId).ToList();
                if(list.Count != 0)
                {
                    return list;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateNote(Note note)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == note.NotesId);
                if (result != null)
                {
                    result.Title = note.Title;
                    result.Body = note.Body;
                    result.Reminder = note.Reminder;
                    result.isArchived = note.isArchived;
                    result.isTrash = note.isTrash;
                    result.isPin = note.isPin;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
