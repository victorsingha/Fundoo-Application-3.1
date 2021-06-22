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
        [AllowAnonymous]
        [HttpPost("add")]
        public ActionResult AddNote(AddNote note)
        {
            try
            {
                this.noteBl.AddNote(note);
                return this.Ok(new { success = true, message = $"Notes Added with UserId: {note.UserId}." });
            }
            catch(Exception e)
            {
                return BadRequest(new { success = false, message = $"No such UserID Exist." });
            }
            
        }
        [AllowAnonymous]
        [HttpGet("list")]
        public ActionResult GetAllNotes(UserIdModel user)
        {
            var data = this.noteBl.GetAllNotes(user.UserId);
            return Ok(data);
        }
    }
}
