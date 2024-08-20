using AutoMapper;
using FileProcess.Api.Contracts.Repositories;
using FileProcess.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FileProcess.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;
        public FilesController(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilesDtoAsync()
        {
            // Get all the files uploaded in the database
            var files = await _fileRepository.GetFilesAsync();

            // Convert entities to dto
            var result = _mapper.Map<IEnumerable<FileDto>>(files);

            return Ok(result);
        }
    }
}
