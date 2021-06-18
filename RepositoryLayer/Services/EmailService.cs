using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Services
{
    public class EmailService
    {
        public static void SendEmail(string email,string link)
        {
            using (SmtpClient client = new SmtpClient("smtp.gmail.com",587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential("sup.pickitup@gmail.com", "pickitup000");

                MailMessage msgObj = new MailMessage();
                msgObj.To.Add("vicunite2@gmail.com");
                msgObj.From = new MailAddress("sup.pickitup@gmail.com");
                msgObj.Subject = "Password Reset Link";
                msgObj.Body = link;
                client.Send(msgObj);
            }
        }
    }
}
