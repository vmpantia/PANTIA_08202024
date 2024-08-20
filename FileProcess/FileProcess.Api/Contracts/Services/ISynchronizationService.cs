using FileProcess.Api.Contracts.Models;

namespace FileProcess.Api.Contracts.Services
{
    public interface ISynchronizationService<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IAuditProperty
    {
        Task DoSyncAsync(IEnumerable<TEntity> entities, CancellationToken token = default);
    }
}