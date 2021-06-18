using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string email,string token)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com",587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("fundoo@gmail.com", "fundooPassword");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add(email);
                msgObj.From = new MailAddress("fundoo@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = token;
                client.Send(msgObj);
            }
        }
    }
}
