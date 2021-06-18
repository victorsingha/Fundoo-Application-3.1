using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        void RegisterUser(User user);
        string AuthenticateUser(string email, string password);
        void ForgotPassword(string email);
    }
}
