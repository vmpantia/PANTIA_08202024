using FileProcess.Api.Constants;
using FileProcess.Api.Contracts.Services;
using System.Net;

namespace FileProcess.Api.Middlewares
{
    public class ApiKeyAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public ApiKeyAuthenticationMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            // Get authentication service use for validating the Api Key
            var authentication = httpContext.RequestServices.GetRequiredService<IAuthenticationService>();

            // Get Api Key stored in the header
            string? apiKey = httpContext.Request.Headers[Common.API_KEY_NAME];

            // Check if the Api Key is valid
            if (!authentication.IsApiKeyValid(apiKey))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await httpContext.Response.WriteAsync("Invalid Api Key provided.");
                return;
            }

            await _next(httpContext);
        }
    }
}
