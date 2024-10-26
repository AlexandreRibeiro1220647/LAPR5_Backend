using System.Net;
using System.Net.Mail;

namespace TodoApi.Services
{
    public class EmailService
    {
        public void SendEmail(string email, string name)
        {

            const string username = "sem5pi-2425g27@outlook.pt"; // replace with your email
            const string password = "-2425g27"; // replace with your password

            var smtpClient = new SmtpClient("smtp.office365.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("sem5pi-2425g27@outlook.pt"),
                Subject = "Application Update",
                Body = GenerateEmailBody(name),
                IsBodyHtml = true,
            };

            mailMessage.To.Add(email);

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