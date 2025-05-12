using Core.Domain.Response;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();


    public virtual async Task<RepositoryResponse> CreateAsync(TEntity entity)
    {
        try
        {
            if (entity == null) return RepositoryResponse.BadRequest("Entity is null.");

            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();

            return RepositoryResponse.Ok();
        }
        catch (Exception ex)
        { return RepositoryResponse.Error(ex.Message); }
    }
    public virtual async Task<RepositoryResponse> ExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            if (expression == null) return RepositoryResponse.BadRequest("Expression is null.");

            var exists = await _dbSet.AnyAsync(expression);
            if (!exists) return RepositoryResponse.NotFound("Entity not found.");

            return RepositoryResponse.Ok();
        }
        catch (Exception ex)
        { return RepositoryResponse.Error(ex.Message); }
    }
    public virtual async Task<RepositoryResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            if (expression == null) return RepositoryResponse<TEntity>.BadRequest("Expression is null.", null);

            var exists = await ExistsAsync(expression);
            if (!exists.Success) return RepositoryResponse<TEntity>.BadRequest("Expression is null.", null);

            var entity = await _dbSet.FirstOrDefaultAsync(expression);

            return RepositoryResponse<TEntity>.Ok(entity);
        }
        catch (Exception ex)
        { return RepositoryResponse<TEntity>.Error(ex.Message, null); }
    }

    public virtual async Task<RepositoryResponse> UpdateAsync(TEntity entity)
    {
        try
        {
            if (entity == null) return RepositoryResponse.BadRequest("Entity is null.");

            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return RepositoryResponse.Ok();
        }
        catch (Exception ex)
        { return RepositoryResponse.Error(ex.Message); }
    }
    public virtual async Task<RepositoryResponse> DeleteAsync(TEntity entity)
    {
        try
        {
            if (entity == null) return RepositoryResponse.BadRequest("Entity is null.");

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();

            return RepositoryResponse.Ok();
        }
        catch (Exception ex)
        { return RepositoryResponse.Error(ex.Message); }
    }

}
