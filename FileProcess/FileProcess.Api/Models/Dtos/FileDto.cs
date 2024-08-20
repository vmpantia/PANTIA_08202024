using System.ComponentModel.DataAnnotations;

namespace FileProcess.Api.Models.Dtos
{
    public class FileDto
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public string ProcessingTimeInSeconds { get; set; }
    }
}
