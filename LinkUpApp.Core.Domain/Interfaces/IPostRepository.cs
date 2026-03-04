using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Infraesctructure.Persistence.Repositories;

namespace LinkUpApp.Core.Domain.Interfaces
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetAllWithCommentsAsync();
    }
}
