using FileProcess.Api.Constants;
using FileProcess.Api.Contracts;

namespace FileProcess.Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        public AuthenticationService(IConfiguration configuration) =>
            _configuration = configuration;

        public bool IsApiKeyValid(string? apiKey)
        {
            // Check if the Api Key is empty
            if (string.IsNullOrWhiteSpace(apiKey)) return false;

            // Get actual Api Key stored in the configuratio nsetting
            var actualApiKey = _configuration.GetValue<string>(Common.API_KEY_NAME);
            return apiKey == actualApiKey;
        }
    }
}
