using System.Collections.Generic;
using System.Linq;
using MediatR;
using MimeKit;

namespace Infrastructure.Services.EmailConfig
{
    public class Message : IRequest<bool>
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public MimeEntity Content { get; set; }

        public Message(IEnumerable<string> to, string subject, MimeEntity content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;        
        }
    }
}