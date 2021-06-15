using CommonLayer;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        public void RegisterUser(User user)
        {
            try
            {

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
