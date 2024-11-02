using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TodoApi.Models.User
{
    public class EmailService
    {
        public void SendEmail(User toUser)
        {
            // Verify that the argument is not null
            if (toUser == null)
            {
                throw new ArgumentNullException(nameof(toUser), "toUser cannot be null");
            }

            const string username = "grupoxlapr@outlook.pt"; // replace with your email
            const string password = "Contaparalapr4"; // replace with your password

            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("grupoxlapr@outlook.pt"),
                Subject = "Application Update",
                Body = GenerateEmailBody(toUser.Name),
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toUser.Email.Value);

            Task.Run(() =>
            {
                try
                {
                    smtpClient.Send(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine("Failed to send email: " + ex.Message);
                }
            });
        }

        private string GenerateEmailBody(string name)
        {
            string candidateName = name;
            string emailBody = $"Hello {candidateName},\n\nYour application status has been updated.\n\nBest regards,\nYour Company";
            return emailBody;
        }
    }
}