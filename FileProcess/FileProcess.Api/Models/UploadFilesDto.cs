namespace FileProcess.Api.Models
{
    public class UploadFilesDto
    {
        public IEnumerable<IFormFile> Files { get; set; }
    }
}
