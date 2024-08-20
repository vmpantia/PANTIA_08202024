using FileProcess.Api.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FileProcess.Api.Repositories
{
    public class FileRepository : BaseRepository<Models.Entities.File>, IFileRepository
    {
        public FileRepository(FileProcessDbContext context) : base(context) { }

        public async Task<IEnumerable<Models.Entities.File>> GetFilesAsync() =>
            await GetAll().ToListAsync();
    }
}
