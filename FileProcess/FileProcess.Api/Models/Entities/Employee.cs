using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FileProcess.Api.Contracts.Models;

namespace FileProcess.Api.Models.Entities
{
    public class Employee : IKey<int>, IAuditProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Gender { get; set; }
        public required string Department { get; set; }

        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
