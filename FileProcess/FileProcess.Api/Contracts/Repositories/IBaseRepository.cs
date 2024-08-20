using System.Linq.Expressions;

namespace FileProcess.Api.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression);
        Task CreateAsync(TEntity entity, CancellationToken token = default, bool autoSave = true);
        Task UpdateAsync(TEntity entity, CancellationToken token = default, bool autoSave = true);
        Task DeleteAsync(TEntity entity, CancellationToken token = default, bool autoSave = true);
    }
}