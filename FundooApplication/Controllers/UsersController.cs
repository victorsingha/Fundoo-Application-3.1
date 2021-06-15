using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        IUserBL userBl;
        public UsersController(IUserBL userBl)
        {
            this.userBl = userBl;
        }
        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            try
            {
                this.userBl.RegisterUser(user);
                return this.Ok(new { success = true, message = $"Registration Successful {user.Email}" });
            }
            catch (Exception e)
            {
                return this.Ok(new { success = false, message = $"Registration Fail {e.Message}" });
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Get", "Working" };
        }

    }
}
