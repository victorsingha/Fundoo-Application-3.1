using BusinessLayer.Interfaces;
using CommonLayer;
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
    public class UsersController : ControllerBase
    {
        IUserBL userBl;
        public UsersController(IUserBL userBl)
        {
            this.userBl = userBl;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public ActionResult RegisterUser(User user)
        {
            try
            {
                this.userBl.RegisterUser(user);
                return this.Ok(new { success = true, message = $"Registration Successful {user.Email}" });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = $"Registration Fail {e.Message}" });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate(UserCredential cred)
        {
            var token = this.userBl.AuthenticateUser(cred.Email,cred.Password);
            if (token == null)
                return Unauthorized();
            return this.Ok(new { success = true, token = token, message = $"Authenticated {cred.Email} {cred.Password}" });
        }

        [HttpGet]
        public string GetUser()
        {
            var UserEmail= User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email",StringComparison.InvariantCultureIgnoreCase));
            return UserEmail.Value;
        }

    }
}
