namespace FileProcess.Api.Contracts.Models
{
    public interface IAuditProperty
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
