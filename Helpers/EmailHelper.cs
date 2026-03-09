using System.Net;
using System.Net.Mail;

namespace CRM.API.Helpers
{
    public class EmailHelper
    {
        public static void SendOtp(string email, string resetLink)
        {
            var fromEmail = "h11434426@gmail.com";
            var appPassword = "lssf zrnf iiqb mmvh";

            var smtp = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, appPassword),
                EnableSsl = true
            };

            var message = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = "CRM Password Reset",
                Body = $"Click this link to reset your password:\n\n{resetLink}",
                IsBodyHtml = false
            };

            message.To.Add(email);

            smtp.Send(message);
        }
    }
}