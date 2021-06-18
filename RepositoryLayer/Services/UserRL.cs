using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        FundooContext _fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            _fundooContext = fundooContext;
        }

        public string AuthenticateUser(string email, string password)
        {
            try
            {
                var result = _fundooContext.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (result == null)
                {
                    return null;
                }
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email",email),
                    new Claim("UserID",result.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }           
        }

        public void ForgotPassword(string email)
        {
            try
            {
                //MessageQueue fundooQueue;

                ////ADD MESSAGE TO QUEUE
                //if (MessageQueue.Exists(@".\Private\FundooQueue"))
                //{
                //    fundooQueue = new MessageQueue(@".\Private\FundooQueue");
                //}
                //else
                //{
                //    fundooQueue = MessageQueue.Create(@".\Private\FundooQueue");
                //}

                //Message MyMessage = new Message();
                //MyMessage.Formatter = new BinaryMessageFormatter();
                //MyMessage.Body = email;
                //MyMessage.Label = "Forget Password Email";
                //fundooQueue.Send(MyMessage);

                //GET MESSAGE FROM QUEUE
                //fundooQueue = new MessageQueue(@".\Private\FundooQueue");
                //Message GetMyMessage = fundooQueue.Receive();
                //GetMyMessage.Formatter = new BinaryMessageFormatter();
                //string emailFromQueue = GetMyMessage.Body.ToString(); 

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RegisterUser(User user)
        {
            try
            {
                _fundooContext.Users.Add(user);
                _fundooContext.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
