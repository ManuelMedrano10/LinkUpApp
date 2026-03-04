using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Domain.Settings;
using LinkUpApp.Infraesctructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkUpApp.Infraesctructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            #region Configurations
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            #endregion

            #region Services IOC
            services.AddScoped<IEmailService, EmailService>();
            #endregion
        }
    }
}
