using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Infrastructure.Services.EmailConfig;
using MediatR;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using Web.EmailSender.Grpc.Protos.SendEmail;

namespace Web.Grpc.Services
{
    public class EmailSenderService : EmailSender.Grpc.Protos.SendEmail.EmailSender.EmailSenderBase
    {
        private readonly ILogger<EmailSenderService> _logger;
        private readonly IMediator _mediator;

        public EmailSenderService(ILogger<EmailSenderService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        public override async Task<Empty> SendEmail(EmailMessageRequest request, ServerCallContext context)
        {
            await _mediator.Send(new Message(request.To.Select(o => o).ToList(), "",
                new TextPart(TextFormat.Html)
                {
                    Text = string.Format(
                        request.Content)
                }));


            return new Empty();
        }
    }
}