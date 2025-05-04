using System.Net;
using System.Net.Mail;

namespace DemoMVCPresentation.Utilities
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com",587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("mohamed.said.33480@gmail.com", "icezolxuuzggplvc");
            Client.Send("mohamed.said.33480@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
