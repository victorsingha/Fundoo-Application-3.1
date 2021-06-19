using BusinessLayer.Interfaces;
using CommonLayer;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public string AuthenticateUser(string email, string password)
        {
            try
            {
                return this.userRL.AuthenticateUser(email, password);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void ChangePassword(string email, string newPassword)
        {
            try
            {
                this.userRL.ChangePassword(email,newPassword);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void RegisterUser(User user)
        {
            try
            {
                this.userRL.RegisterUser(user);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
