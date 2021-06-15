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
