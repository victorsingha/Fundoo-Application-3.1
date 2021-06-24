using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
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

        public void DeleteNote(int noteId)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    _fundooContext.Notes.Remove(result);
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

        public List<NoteResponse> GetAllNotes(int UserId)
        {
            try
            {
                var list = _fundooContext.Notes.Where(e => e.UserId == UserId).ToList();
                if(list.Count != 0)
                {
                    List<NoteResponse> response = new List<NoteResponse>();
                    foreach(var note in list)
                    {
                        NoteResponse noteResponse = new NoteResponse();
                        noteResponse.NotesId = note.NotesId;
                        noteResponse.Title = note.Title;
                        noteResponse.Body = note.Body;
                        noteResponse.Reminder = note.Reminder;
                        noteResponse.Color = note.Color;
                        noteResponse.isTrash = note.isTrash;
                        noteResponse.isArchived = note.isArchived;
                        noteResponse.isPin = note.isPin;
                        noteResponse.UserId = note.UserId;

                        response.Add(noteResponse);
                    }
                    return response;
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateArchived(int noteId, bool isArchived)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.isArchived = isArchived;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateBody(int noteId, string body)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Body = body;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateColor(int noteId, string color)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Color = color;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
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

        public void UpdatePin(int noteId, bool isPin)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.isPin = isPin;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateReminder(int noteId, string reminder)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Reminder = reminder;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateTitle(int noteId,string title)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.Title = title;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateTrash(int noteId, bool isTrash)
        {
            try
            {
                var result = _fundooContext.Notes.FirstOrDefault(u => u.NotesId == noteId);
                if (result != null)
                {
                    result.isTrash = isTrash;
                    _fundooContext.SaveChanges();
                }
                else
                {
                    throw new Exception("No such NoteId Exist");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
