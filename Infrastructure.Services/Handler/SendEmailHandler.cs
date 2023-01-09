using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.Domain.Exceptions;
using Core.Domain.Model.MessageBroker;
using Infrastructure.Services.EmailConfig;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services.Handler
{
    public class SendEmailHandler : IRequestHandler<MessageBody<EmailMessageModel>, bool>
    {
        private readonly EmailConfiguration _emailConfig;

        public SendEmailHandler(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }


        public async Task<bool> Handle(MessageBody<EmailMessageModel> request,
            CancellationToken cancellationToken)
        {
            var emailMessage = CreateEmailMessage(new Message(request.Data.To.Select(o => o).ToList(), "",
                new TextPart(TextFormat.Html)
                {
                    Text = string.Format(
                        request.Data.Content)
                }));

            await SendAsync(emailMessage);
            return true;
        }


        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Email Sender", _emailConfig.From));

            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Content;

            return emailMessage;
        }


        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);
                }
                catch (Exception e)
                {
                    throw new APIException(
                        client.IsAuthenticated + e.Message, HttpStatusCode.NotAcceptable);
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}