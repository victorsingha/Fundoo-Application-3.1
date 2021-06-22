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
                 return _fundooContext.Notes.ToList();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
