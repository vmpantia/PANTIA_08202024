namespace FileProcess.Api.Contracts.Services
{
    public interface IFileService
    {
        Task<string> ReadAllLinesAsync(IFormFile file);
    }
}