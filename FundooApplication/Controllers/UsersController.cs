using BusinessLayer.Interfaces;
using CommonLayer;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooApplication.Controllers
{
    //[EnableCors("AllowOrigin")]
    //[EnableCors]
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
            try
            {
                var token = this.userBl.AuthenticateUser(cred.Email, cred.Password);
                if (token == null)
                    return Unauthorized();
                return this.Ok(new { success = true, token = token, message = $"Authenticated {cred.Email}" });
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        [HttpGet]
        public string GetUser()
        {
            var UserEmail= User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email",StringComparison.InvariantCultureIgnoreCase));
            var UserID= User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID",StringComparison.InvariantCultureIgnoreCase));
            return $" CLAIMS Email:{UserEmail.Value} UserID:{UserID.Value}";
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public ActionResult ForgotPassword(UserEmail user)
        {
            try
            {
                bool isExist = this.userBl.ForgotPassword(user.Email);
                if (isExist) return Ok(new { success = true, message = $"Reset Link sent to {user.Email}"});
                else return BadRequest(new { success = false, message = $"No user Exist with {user.Email}" });

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
   
        [HttpPut("reset-password")]
        public ActionResult ResetPassword(UserNewPassword user)
        {
            try
            {  
                if(user.NewPassword != user.ConfirmPassword)
                {
                    return Ok(new { success = false, message = "New Password and Confirm Password are not equal." });
                }
                var UserEmailObject = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("Email", StringComparison.InvariantCultureIgnoreCase));
                this.userBl.ChangePassword(UserEmailObject.Value,user.NewPassword);
                //return Ok($"Updated Email: {UserEmailObject.Value} NewPassword: {user.Password}");
                return Ok(new { success = true,message = "Password Changed Sucessfully",email = $"{UserEmailObject.Value}" });
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
