using LinkUpApp.Core.Domain.Entities;
using LinkUpApp.Core.Domain.Interfaces;
using LinkUpApp.Infraesctructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly LinkUpContext _context;

        public PostRepository(LinkUpContext context) 
            : base(context) 
        {
            _context = context;
        }
        public async Task<List<Post>> GetAllWithCommentsAsync()
        {
            return await _context.Set<Post>()
                .Include(p => p.Comments ?? new List<Comment>())
                    .ThenInclude(c => c.Replies)
                .Include(p => p.Reactions)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }
    }
}
