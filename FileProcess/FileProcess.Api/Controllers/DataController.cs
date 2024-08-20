using FileProcess.Api.Contracts.Repositories;
using FileProcess.Api.Contracts.Services;
using FileProcess.Api.Extensions;
using FileProcess.Api.Models.Dtos;
using FileProcess.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FileProcess.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly ISynchronizationService<Employee, int> _synchronizationService;
        private readonly IFileService _fileService;
        private readonly IFileRepository _fileRepository;
        public DataController(ISynchronizationService<Employee, int> synchronizationService,
                              IFileService fileService,
                              IFileRepository fileRepository)
        {
            _synchronizationService = synchronizationService;
            _fileService = fileService;
            _fileRepository = fileRepository;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportData([FromForm] UploadFileDto request)
        {
            // Check if the file is a valid json file
            if (!request.File.IsValidJsonFile())
                return BadRequest("Uploaded file is not a vaild Json file.");

            // Prepare file upload request
            var fileUpload = new Models.Entities.File
            {
                Id = Guid.NewGuid(),
                Name = request.File.FileName,
                Extension = Path.GetExtension(request.File.FileName),
                StartAt = DateTime.Now,
            };

            // Get all the values inside of the file
            var values = await _fileService.ReadAllLinesAsync(request.File);

            // Deserialize values to list of employees
            var latestEmployees = JsonSerializer.Deserialize<IEnumerable<Employee>>(values.ToString()) ?? new List<Employee>();
            
            // Process data synchronization in the database
            await _synchronizationService.DoSyncAsync(latestEmployees);

            // Create a file upload record
            fileUpload.FinishedAt = DateTime.Now;
            await _fileRepository.CreateAsync(fileUpload);

            return Ok("Successfully import employees in the database.");
        }
    }
}
