namespace FileProcess.Api.Contracts.Repositories
{
    public interface IFileRepository : IBaseRepository<Api.Models.Entities.File>
    {
        Task<IEnumerable<Api.Models.Entities.File>> GetFiles();
    }
}