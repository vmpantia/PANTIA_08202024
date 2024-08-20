using System.Linq.Expressions;

namespace FileProcess.Api.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity entity, CancellationToken token);
        Task DeleteAsync(TEntity entity, CancellationToken token);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression);
        Task UpdateAsync(TEntity entity, CancellationToken token);
    }
}