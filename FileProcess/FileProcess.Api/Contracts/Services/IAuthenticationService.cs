namespace FileProcess.Api.Contracts.Services
{
    public interface IAuthenticationService
    {
        bool IsApiKeyValid(string? apiKey);
    }
}
