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
            this.noteBl.AddNote(note);
            return Ok("success");
        }
        [AllowAnonymous]
        [HttpGet("list")]
        public ActionResult GetAllNotes(AddNote note)
        {
            var data = this.noteBl.GetAllNotes(note.UserId);
            return Ok(data);
        }
    }
}
