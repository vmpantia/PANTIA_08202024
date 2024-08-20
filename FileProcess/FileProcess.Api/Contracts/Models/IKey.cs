namespace FileProcess.Api.Contracts.Models
{
    public interface IKey<TProperty>
    {
        TProperty Id { get; set; }
    }
}
