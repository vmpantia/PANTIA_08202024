using FileProcess.Api.Contracts.Models;

namespace FileProcess.Api.Contracts.Services
{
    public interface ISynchronizationService<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IAuditProperty
        where TKey : IKey<TKey>
    {
        Task DoSyncAsync(IEnumerable<TEntity> entities, CancellationToken token);
    }
}