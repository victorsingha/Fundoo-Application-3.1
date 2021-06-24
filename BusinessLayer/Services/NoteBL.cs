using BusinessLayer.Interfaces;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using CommonLayer.ResponseModel;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public void AddNote(AddNote note)
        {
            try
            {
                this.noteRL.AddNote(note);
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
                this.noteRL.DeleteNote(noteId);
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
                return this.noteRL.GetAllNotes(UserId);
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
                this.noteRL.UpdateBody(noteId,body);
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
                this.noteRL.UpdateNote(note);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateTitle(int noteId,string title)
        {
            try
            {
                this.noteRL.UpdateTitle(noteId,title);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
