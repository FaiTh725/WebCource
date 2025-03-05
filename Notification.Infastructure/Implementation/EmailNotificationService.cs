using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using Notification.Application.Interfaces;
using Notification.Contracts.Email.Requests;
using Application.Shared.Exceptions;
using Notification.Infastructure.Helpers.Email;

namespace Notification.Infastructure.Implementation
{
    public class EmailNotificationService : INotificationService<SendEmailRequest>
    {
        private readonly ILogger<EmailNotificationService> logger;
        private SmtpSetting smtpSetting;

        public EmailNotificationService(
            ILogger<EmailNotificationService> logger,
            IConfiguration configuration)
        {
            this.logger = logger;

            smtpSetting = configuration
                .GetSection("EmailSettings")
                .Get<SmtpSetting>()
                ?? throw new AppConfigurationException("EmailSettings Section");
        }

        public async Task SendNotification(SendEmailRequest message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(
                "FaiTh Tests", 
                smtpSetting.ReciverEmail));
            emailMessage.To.Add(new MailboxAddress("", message.Consumer));
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart()
            {
                Text = message.Message
            };
            
            try
            {
                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.mail.ru", 465);
                await client.AuthenticateAsync(
                    smtpSetting.ReciverEmail, 
                    smtpSetting.Password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
            catch
            {
                logger.LogError("Error Send Email to " + message.Consumer);
                return;
            }

            logger.LogInformation("Email has already sent to " + message.Consumer);
        }
    }
}
