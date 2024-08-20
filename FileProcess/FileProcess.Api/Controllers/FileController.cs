using FileProcess.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FileProcess.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        public FileController() { }

        [HttpPost]
        public IActionResult FileProcess([FromForm] UploadFileDto request)
        {
            return Ok();
        }
    }
}
