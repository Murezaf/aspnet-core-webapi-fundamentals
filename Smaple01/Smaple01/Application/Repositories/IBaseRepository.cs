namespace Smaple01.Application.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<bool> SaveChangesAsync();
    }
}
