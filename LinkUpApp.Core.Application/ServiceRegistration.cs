using LinkUpApp.Core.Application.Interfaces;
using LinkUpApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LinkUpApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayerIoc(this IServiceCollection services)
        {
            #region Configurations
            services.AddAutoMapper(config => config.AddMaps(Assembly.GetExecutingAssembly()));
            #endregion
            #region Services IOC
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IFriendshipService, FrienshipService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReplyService, ReplyService>();
            services.AddScoped<IBattleshipService, BattleshipService>();
            #endregion
        }
    }
}
