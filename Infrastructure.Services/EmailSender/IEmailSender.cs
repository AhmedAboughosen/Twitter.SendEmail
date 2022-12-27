using System.Threading.Tasks;

namespace Infrastructure.Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}