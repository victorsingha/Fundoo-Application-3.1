using BusinessLayer.Interfaces;
using CommonLayer.DatabaseModel;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INoteBL noteBl;
        public NotesController(INoteBL noteBl)
        {
            this.noteBl = noteBl;
        }

        [HttpPost("add")]
        public ActionResult AddNote(AddNote note)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                note.UserId = Int32.Parse(userId.Value);
                this.noteBl.AddNote(note);
                return this.Ok(new { success = true, message = $"Notes Added with UserId: {note.UserId}." });
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, message = $"No such UserID Exist." });
            }          
        }
     
        [HttpGet("list")]
        public ActionResult GetAllNotes()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                if(userId == null) return BadRequest(new { success = false, message = $"Unauthorised" });
                var result = this.noteBl.GetAllNotes(Int32.Parse(userId.Value));
                if (result != null) return this.Ok(result);
                return BadRequest(new { success = false, message = $"No such UserID Exist." });
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpPut("update")]
        public ActionResult UpdateNote(Note note)
        {
            try
            {
                this.noteBl.UpdateNote(note);
                return Ok(new { success = true, message = $"Note Updated." });
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, message = $"No such NoteID Exist." });
            }
        }
    
        [HttpDelete("delete/{noteId}")]
        public ActionResult DeleteNote(int noteId)
        {
            try
            {
                this.noteBl.DeleteNote(noteId);
                return Ok(new { success = true, message = $"Note Deleted Permanently" });
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, message = $"Delete Fail." });
            }
        }

        [HttpPut("title/{noteId}")]
        public ActionResult UpdateTitle(int noteId,NoteTitle noteTitle)
        {
            try
            {
                this.noteBl.UpdateTitle(noteId,noteTitle.Title);
                return Ok(new { success = true, message = $"Title Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }

        [HttpPut("body/{noteId}")]
        public ActionResult UpdateBody(int noteId, NoteBody noteBody)
        {
            try
            {
                this.noteBl.UpdateBody(noteId, noteBody.Body);
                return Ok(new { success = true, message = $"Body Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }

        [HttpPut("reminder/{noteId}")]
        public ActionResult UpdateReminder(int noteId, NoteReminder noteReminder)
        {
            try
            {
                this.noteBl.UpdateReminder(noteId, noteReminder.Reminder);
                return Ok(new { success = true, message = $"Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }
        [HttpPut("color/{noteId}")]
        public ActionResult UpdateColor(int noteId, NoteColor noteColor)
        {
            try
            {
                this.noteBl.UpdateColor(noteId, noteColor.Color);
                return Ok(new { success = true, message = $"Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }
        [HttpPut("archived/{noteId}")]
        public ActionResult UpdateArchived(int noteId, NoteArchived noteArchived)
        {
            try
            {
                this.noteBl.UpdateArchived(noteId, noteArchived.isArchived);
                return Ok(new { success = true, message = $"Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }
        [HttpPut("trash/{noteId}")]
        public ActionResult UpdateTrash(int noteId, NoteTrash noteTrash)
        {
            try
            {
                this.noteBl.UpdateTrash(noteId, noteTrash.isTrash);
                return Ok(new { success = true, message = $"Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }
        [HttpPut("pin/{noteId}")]
        public ActionResult UpdatePin(int noteId, NotePin notePin)
        {
            try
            {
                this.noteBl.UpdatePin(noteId, notePin.isPin);
                return Ok(new { success = true, message = $"Updated" });
            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = $"Update Fail." });
            }
        }
    }
}
