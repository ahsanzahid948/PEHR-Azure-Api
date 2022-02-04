using Application.Interfaces.Services;
using Infrastructure.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            //services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IPasswordService, PasswordService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}