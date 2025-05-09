using Core.Domain.Response;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepositoryResponse> CreateAsync(TEntity entity);
        Task<RepositoryResponse> DeleteAsync(TEntity entity);
        Task<RepositoryResponse> ExistsAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepositoryResponse<TEntity>> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<RepositoryResponse> UpdateAsync(TEntity entity);
    }
}