namespace FileProcess.Api.Contracts
{
    public interface IAuthenticationService
    {
        bool IsApiKeyValid(string? apiKey);
    }
}
