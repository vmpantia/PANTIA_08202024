using System.ComponentModel.DataAnnotations;

namespace FileProcess.Api.Models.Entities
{
    public class File
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime FinishedAt { get; set; }
    }
}
