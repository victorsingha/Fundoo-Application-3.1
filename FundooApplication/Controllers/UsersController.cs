using BusinessLayer.Interfaces;
using CommonLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
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
            var UserID= User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID",StringComparison.InvariantCultureIgnoreCase));
            return $"Email:{UserEmail.Value} UserID:{UserID.Value}";
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(User user)
        {
            try
            {
                this.userBl.ForgotPassword(user.Email);
                return Ok(user.Email);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
   
        [HttpPut("reset-password")]
        public ActionResult ResetPassword(User user)
        {
            try
            {  
                var UserEmailObject = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
                this.userBl.ChangePassword(user.Email,user.Password);
                return Ok($"Updated Email: {UserEmailObject.Value} NewPassword: {user.Password}");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
