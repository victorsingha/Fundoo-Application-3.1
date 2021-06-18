using CommonLayer;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
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
                MessageQueue fundooQueue;

                //ADD MESSAGE TO QUEUE
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    fundooQueue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    fundooQueue = MessageQueue.Create(@".\Private$\FundooQueue");
                }

                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = email;
                MyMessage.Label = "Forget Password Email";
                fundooQueue.Send(MyMessage);

                //GET MESSAGE FROM QUEUE
                fundooQueue = new MessageQueue(@".\Private$\FundooQueue");
                Message GetMyMessage = fundooQueue.Receive();
                GetMyMessage.Formatter = new BinaryMessageFormatter();
                string emailFromQueue = GetMyMessage.Body.ToString();


                //create message queue instance
                fundooQueue = new MessageQueue(@".\private$\FundooQueue");
                //set formatter same as sender
                fundooQueue.Formatter = new BinaryMessageFormatter();
                fundooQueue.MessageReadPropertyFilter.SetAll();
                //Raise receive completed event 
                fundooQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);
                //start receiving messages
                fundooQueue.BeginReceive();
                //In msmqQueue_ReceiveCompleted
                //Extract the actual message 
                string emailMsg = GetMyMessage.Body.ToString();               
                //Create mail message instance 
                //EmailService.SendEmail(email, GenerateToken(emailMsg));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            throw new NotImplementedException();
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

        //GENERATE TOKEN WITH EMAIL
        public string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email",email)
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
    }
}
