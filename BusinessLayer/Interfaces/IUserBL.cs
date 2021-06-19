using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        void RegisterUser(User user);
        string AuthenticateUser(string email, string password);
        bool ForgotPassword(string email);
        void ChangePassword(string email, string newPassword);
    }
}
