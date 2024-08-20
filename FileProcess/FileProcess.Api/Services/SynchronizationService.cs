using AutoMapper;
using FileProcess.Api.Contracts.Models;
using FileProcess.Api.Contracts.Repositories;
using FileProcess.Api.Contracts.Services;
using FileProcess.Api.Models.Enums;
using FileProcess.Api.Models.Sync;

namespace FileProcess.Api.Services
{
    public class SynchronizationService<TEntity, TKey> : ISynchronizationService<TEntity, TKey> 
        where TEntity : class, IKey<TKey>, IAuditProperty
        where TKey : IKey<TKey>
    {
        private readonly IUnitOfWork<TEntity> _uow;
        private readonly IMapper _mapper;
        public SynchronizationService(IUnitOfWork<TEntity> uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task DoSyncAsync(IEnumerable<TEntity> entities, CancellationToken token)
        {
            // Skip if the parameter entities is empty
            if (!entities.Any()) return;

            // Get current and new entities
            var currentEntities = _uow.Repository.GetAll().ToList();
            var newEntities = entities.ToList();
            var toBeSync = GetDataToBeSync(currentEntities, newEntities);

            foreach (var sync in toBeSync)
            {
                switch (sync.Action)
                {
                    case SyncAction.Create:
                        {
                            var toBeCreate = newEntities.First(data => data.Id!.Equals(sync.Id));
                            await _uow.Repository.CreateAsync(toBeCreate, token, false);
                            break;
                        }
                    case SyncAction.Update:
                        {
                            var currentEntity = currentEntities.First(data => data.Id!.Equals(sync.Id));
                            var newEntity = newEntities.First(data => data.Id!.Equals(sync.Id));
                            var toBeUpdate = _mapper.Map(newEntity, currentEntity);
                            await _uow.Repository.UpdateAsync(toBeUpdate, token, false);
                            break;
                        }
                    case SyncAction.Delete:
                        {
                            var toBeDelete = currentEntities.First(data => data.Id!.Equals(sync.Id));
                            await _uow.Repository.DeleteAsync(toBeDelete, token, false);
                            break;
                        }
                }
            }

            await _uow.SaveAsync(token);
        }

        private List<DataSync<TKey>> GetDataToBeSync(List<TEntity> currentEntities, List<TEntity> newEntities)
        {
            // Get current ids
            var currentIds = currentEntities.Select(data => data.Id).ToList();
            var newIds = newEntities.Select(data => data.Id).ToList();

            // Get entities to be sync based on their action
            var toBeSync = newIds.Except(currentIds).Select(data => new DataSync<TKey>(SyncAction.Create, data)) // Create
                                                    .Concat(newIds.Intersect(currentIds).Select(data => new DataSync<TKey>(SyncAction.Update, data))) // Update
                                                    .Concat(currentIds.Except(newIds).Select(data => new DataSync<TKey>(SyncAction.Delete, data))) // Delete
                                                    .ToList();
            return toBeSync;
        }
    }
}
