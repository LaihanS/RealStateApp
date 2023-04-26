namespace Banking.Infrastructure.Persistence.Repositories
{
    public interface IGenericRepository<Dto, Entity > 
        where Dto : class
        where Entity : class

    {
        Task<Dto> AddAsync(Dto entity);
        Task DeleteAsync(Dto entity, string id);
        Task EditAsync(Dto entity, string id);
        Task<List<Dto>> GetAsync();
        Task<List<Dto>> GetAsyncWithJoin(List<string> navProperties);
        Task<Dto> GetByidAsync(string id);
    }
}