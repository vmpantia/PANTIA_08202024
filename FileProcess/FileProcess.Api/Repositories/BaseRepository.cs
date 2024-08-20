using FileProcess.Api.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FileProcess.Api.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly FileProcessDbContext _context;
        private readonly DbSet<TEntity> _table;
        public BaseRepository(FileProcessDbContext context)
        {
            _context = context;
            _table = context.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll() =>
            _table;

        public IQueryable<TEntity> GetByExpression(Expression<Func<TEntity, bool>> expression) =>
            _table.Where(expression);

        public async Task CreateAsync(TEntity entity, CancellationToken token, bool autoSave = true)
        {
            await _table.AddAsync(entity, token);
            if (autoSave) await _context.SaveChangesAsync(token);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken token, bool autoSave = true)
        {
            _table.Update(entity);
            if (autoSave) await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken token, bool autoSave = true)
        {
            _table.Remove(entity);
            if (autoSave) await _context.SaveChangesAsync(token);
        }
    }
}
