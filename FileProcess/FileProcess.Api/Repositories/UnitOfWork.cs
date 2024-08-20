using FileProcess.Api.Contracts.Repositories;

namespace FileProcess.Api.Repositories
{
    public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
    {
        private readonly FileProcessDbContext _context;
        public UnitOfWork(FileProcessDbContext context) => _context = context;

        public IBaseRepository<TEntity> Repository => new BaseRepository<TEntity>(_context);
        public async Task SaveAsync(CancellationToken token) => await _context.SaveChangesAsync(token);
    }
}
