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
    {
        private readonly IUnitOfWork<TEntity> _uow;
        private readonly IMapper _mapper;
        private readonly ILoggerService<SynchronizationService<TEntity, TKey>> _logger;
        public SynchronizationService(IUnitOfWork<TEntity> uow, 
                                      IMapper mapper, 
                                      ILoggerService<SynchronizationService<TEntity, TKey>> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task DoSyncAsync(IEnumerable<TEntity> entities, CancellationToken token = default)
        {
            try
            {
                // Get current and new entities
                var currentEntities = _uow.Repository.GetAll().ToList();
                var newEntities = entities.ToList();
                var toBeSync = GetDataToBeSync(currentEntities, newEntities);

                _logger.LogInfo($"Summaries for the data to be sync in the database. " +
                    $"Create [{toBeSync.Count(data => data.Action == SyncAction.Create)}] " +
                    $"Update [{toBeSync.Count(data => data.Action == SyncAction.Update)}] " +
                    $"Delete [{toBeSync.Count(data => data.Action == SyncAction.Delete)}]");

                foreach (var sync in toBeSync)
                {
                    switch (sync.Action)
                    {
                        case SyncAction.Create:
                            {
                                var toBeCreate = newEntities.First(data => data.Id!.Equals(sync.Id));
                                toBeCreate.CreatedAt = DateTime.Now; // Update audit property
                                await _uow.Repository.CreateAsync(toBeCreate, token, false);
                                break;
                            }
                        case SyncAction.Update:
                            {
                                var currentEntity = currentEntities.First(data => data.Id!.Equals(sync.Id));
                                var newEntity = newEntities.First(data => data.Id!.Equals(sync.Id));
                                var toBeUpdate = _mapper.Map(newEntity, currentEntity);
                                toBeUpdate.UpdatedAt = DateTime.Now; // Update audit property
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
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
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
