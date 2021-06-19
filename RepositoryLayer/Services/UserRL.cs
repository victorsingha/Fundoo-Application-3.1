using CommonLayer;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
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
                string encryptedPassword = StringCipher.Encrypt(password);
                var result = _fundooContext.Users.FirstOrDefault(u => u.Email == email && u.Password == encryptedPassword);
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
                MessageQueue queue;

                //ADD MESSAGE TO QUEUE
                if (MessageQueue.Exists(@".\Private$\FundooQueue"))
                {
                    queue = new MessageQueue(@".\Private$\FundooQueue");
                }
                else
                {
                    queue = MessageQueue.Create(@".\Private$\FundooQueue");
                }

                Message MyMessage = new Message();
                MyMessage.Formatter = new BinaryMessageFormatter();
                MyMessage.Body = email;
                MyMessage.Label = "Forget Password Email";
                queue.Send(MyMessage);
                Message msg = queue.Receive();
                msg.Formatter = new BinaryMessageFormatter();
                EmailService.SendEmail(msg.Body.ToString(), GenerateToken(msg.Body.ToString()));
                queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                queue.BeginReceive();
                queue.Close();

                //GET MESSAGE FROM QUEUE
                //fundooQueue = new MessageQueue(@".\Private$\FundooQueue");
                //Message GetMyMessage = fundooQueue.Receive();
                //GetMyMessage.Formatter = new BinaryMessageFormatter();
                //string emailFromQueue = GetMyMessage.Body.ToString();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);                
                EmailService.SendEmail(e.Message.ToString(),GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch(Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            
        }

        public void RegisterUser(User user)
        {
            try
            {
                string encryptedPassword = StringCipher.Encrypt(user.Password);
                user.Password = encryptedPassword;
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

        public void ChangePassword(string email, string newPassword)
        {
            try
            {
                var result = _fundooContext.Users.FirstOrDefault(u => u.Email == email);
                if (result != null)
                {
                    string encryptedPassword = StringCipher.Encrypt(newPassword);
                    result.Password = encryptedPassword;
                    _fundooContext.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
