using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace FacilityManager.BusinessLogic.Helpers
{
    public class EmailHelper
    {
        private readonly IConfiguration _configuration;

        public EmailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendUserVerificationEmail(string email, string token)
        {
            var subject = "Facility Manager - Verification Link";
            var link = _configuration["FacilityManagerLink"]
                     + "/auth"
                     + $"?email={email}"
                     + $"&token={token}"; ;

            var body = String.Format($@"<p>Hello,<br>
                            Please confirm your account by clicking this link: 
                            <a href=""{link}"">confirm email</a></p>");

            SendEmail(email, subject, body);
        }

        public void SendResetPasswordLink(string email, string token)
        {
            var subject = "Facility Manager - Reset Password Link";
            var link = _configuration["FacilityManagerLink"]
                     + "/reset-password-step2"
                     + $"?email={email}"
                     + $"&token={token}";

            var body = String.Format($@"<p>Hello,<br>
                            Please reset your password by clicking this link: 
                            <a href=""{link}"">reset password</a></p>");

            SendEmail(email, subject, body);
        }

        private void SendEmail(string email, string subject, string body)
        {
            SmtpClient client = new SmtpClient(_configuration["SMTP_Server"]);
            client.Port = int.Parse(_configuration["SMTP_Port"]);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(_configuration["Email_Login"], _configuration["Email_Password"]);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("ebrit1999@onet.pl");
            mailMessage.To.Add(email);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;
            client.Send(mailMessage);
        }
    }
}