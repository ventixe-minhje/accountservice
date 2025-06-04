using Data.Data;
using Microsoft.EntityFrameworkCore;
using Models.Repositories;
using System.Linq.Expressions;

namespace Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;

    protected BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }

        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, StatusCode = 400, Error = ex.Message };
        }
    }
    public virtual async Task<RepositoryResult> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(expression) ?? throw new Exception("Not found.");
            return new RepositoryResult<TEntity?> { Success = true, Result = entity };
        }

        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?> { Success = false, StatusCode = 400, Error = ex.Message };
        }
    }
    public virtual async Task<RepositoryResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        var result = await _table.AnyAsync(expression);
        return result ? new RepositoryResult { Success = true } : new RepositoryResult { Success = false, Error = "Not found." };
    }
    public virtual async Task<RepositoryResult> UpdateAsync(TEntity entity)
    {
        try
        {
            _table.Update(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }

        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, StatusCode = 400, Error = ex.Message };
        }
    }
    public virtual async Task<RepositoryResult> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }

        catch (Exception ex)
        {
            return new RepositoryResult { Success = false, StatusCode = 400, Error = ex.Message };
        }
    }
}
