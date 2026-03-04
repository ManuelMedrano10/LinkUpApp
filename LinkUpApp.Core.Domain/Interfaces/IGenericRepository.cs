
namespace LinkUpApp.Infraesctructure.Persistence.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity?> AddAsync(Entity entity);
        Task<List<Entity?>> AddRangeAsync(List<Entity?> entities);
        Task DeleteAsync(int id);
        Task<List<Entity>> GetAllList();
        Task<List<Entity>> GetAllListWithIncludeAsync(List<string> properties);
        IQueryable<Entity> GetAllQuery();
        IQueryable<Entity> GetAllQueryWithInclude(List<string> properties);
        Task<Entity?> GetById(int id);
        Task<Entity?> UpdateAsync(int id, Entity entity);
    }
}