using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zika.Services
{
    //Mailing
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(
                "Zika Express",
                "info@zikaexpresslogistics.com"
            ));
            mimeMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : "Hello From Zika Express";
            mimeMessage.Cc.Add(new MailboxAddress(email));
            mimeMessage.Bcc.Add(new MailboxAddress("Zika Express", "zikaexpress01@gmail.com"));
            BodyBuilder builder = new BodyBuilder
            {
                HtmlBody = message
            };
            mimeMessage.Body = builder.ToMessageBody() ?? new TextPart("plain");

            using (var client = new SmtpClient())
            {
                client.Connect("zikaexpresslogistics.com", 25, SecureSocketOptions.None);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("info@zikaexpresslogistics.com", "Zikalogistics01");
                await client.SendAsync(mimeMessage);
                _logger.LogInformation("message sent successfully...");
                await client.DisconnectAsync(true);
            }

        }

        public async Task SendEmailToAllAsync(string[] emails, string subject, string message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(
                "Zika Express",
                "info@zikaexpresslogistics.com"
            ));
            mimeMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : "Hello From Zika Express";
            mimeMessage.Bcc.Add(new MailboxAddress("Zika Express", "zikaexpress01@gmail.com"));

            foreach (string email in emails)
            {
                mimeMessage.Bcc.Add(new MailboxAddress(email));
            }
            
            mimeMessage.Subject = !string.IsNullOrEmpty(subject) ? subject : "Hello From Zika";
            BodyBuilder builder = new BodyBuilder
            {
                HtmlBody = message
            };
            mimeMessage.Body = builder.ToMessageBody() ?? new TextPart("plain");

            using (var client = new SmtpClient())
            {
                client.Connect("zikaexpresslogistics.com", 25, SecureSocketOptions.None);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("info@zikaexpresslogistics.com", "Zikalogistics01");
                await client.SendAsync(mimeMessage);
                _logger.LogInformation("message sent successfully...");
                await client.DisconnectAsync(true);
            }
        }
    }
}
