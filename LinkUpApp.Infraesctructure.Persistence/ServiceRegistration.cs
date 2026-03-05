using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;
using LinkUpApp.Infraesctructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkUpApp.Infraesctructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayerIoc(this IServiceCollection services, IConfiguration config)
        {
            #region Contexts
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<LinkUpContext>(opt =>
                                              opt.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<LinkUpContext>(
                  (serviceProvider, opt) =>
                  {
                      opt.EnableSensitiveDataLogging();
                      opt.UseSqlServer(connectionString,
                      m => m.MigrationsAssembly(typeof(LinkUpContext).Assembly.FullName));
                  },
                    contextLifetime: ServiceLifetime.Scoped,
                    optionsLifetime: ServiceLifetime.Scoped
                 );

                #endregion

                #region Repositories IOC
                services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
                services.AddScoped<IPostRepository, PostRepository>();
                services.AddScoped<IFriendshipRepository, FriendshipRepository>();
                services.AddScoped<IReactionRepository, ReactionRepository>();
                services.AddScoped<IReplyRepository, ReplyRepository>();
                services.AddScoped<ICommentRepository, CommentRepository>();
                services.AddScoped<IGameRepository, GameRepository>();
                services.AddScoped<IShipPositionRepository, ShipPositionRepository>();
                services.AddScoped<IShotRepository, ShotRepository>();
                #endregion
            }
        }
    }
}
