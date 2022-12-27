using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services.EmailSender
{
    public static class EmailServiceContainer
    {
        public static void AddEmailServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}