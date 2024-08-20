using FileProcess.Api.Contracts.Services;
using System.Text;

namespace FileProcess.Api.Services
{
    public class FileService : IFileService
    {
        private readonly ILoggerService<FileService> _logger;
        public FileService(ILoggerService<FileService> logger) =>
            _logger = logger;

        public async Task<string> ReadAllLinesAsync(IFormFile file)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    int count = 0;
                    var values = new StringBuilder();
                    _logger.LogInfo($"Start reading the file {file.FileName}");
                    while (reader.Peek() >= 0)
                    {
                        count++;

                        // Read current line value
                        var currentLine = await reader.ReadLineAsync();

                        // Append the currentLine value in the values
                        values.Append(currentLine);
                    }
                    _logger.LogInfo($"Completed reading all the lines, {count} records found in the file.");
                    return values.ToString();
                }
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
