using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
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
