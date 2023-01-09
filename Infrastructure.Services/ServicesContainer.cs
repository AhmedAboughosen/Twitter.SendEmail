using System.Reflection;
using Core.Application.Contracts.Services.BaseService;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    public static class ServicesContainer
    {
        public static void AddServicesRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IMessageHandler, BaseService.MessageHandler>();

        }
    }
}