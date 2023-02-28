using CI_platform.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CI_platform.Service
{
    public class EmailService : IEmailService
    {
        private const string templatepath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        //UserEmailOptions _emailOptions = new UserEmailOptions()
        //{
        //    Body = "Abcd",
        //    Subject = "Ci-platform",
        //    ToEmails = new List<string>()
        //};



        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential,
            };

            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);

        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatepath, templateName));
            return body;
        }
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = "This is test email";
            userEmailOptions.Body = GetEmailBody("TestEmail");
            await SendEmail(userEmailOptions);
        }
    }
}
