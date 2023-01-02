using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class EmailServiceContainer
    {
        public static void AddEmailServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            
            // services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}