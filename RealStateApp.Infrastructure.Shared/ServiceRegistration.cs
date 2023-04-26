
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateApp.Core.Application.IServices;
using RealStateApp.Core.Domain.Settings;
using RealStateApp.Infrastructure.Shared.EmailServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateApp.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<EmailSettings>(configuration.GetSection("MailSettings"));

            services.AddTransient<IEmailService, EmailService>();

        }
    }
}
