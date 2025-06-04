using Models.Repositories;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepositoryResult> AddAsync(TEntity entity);
        Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepositoryResult> DeleteAsync(TEntity entity);
        Task<RepositoryResult> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepositoryResult> UpdateAsync(TEntity entity);
    }
}