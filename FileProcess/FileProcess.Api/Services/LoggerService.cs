using FileProcess.Api.Contracts.Services;

namespace FileProcess.Api.Services
{
    public class LoggerService<TService> : ILoggerService<TService> where TService : class
    {
        private readonly ILogger<TService> _logger;
        public LoggerService(ILogger<TService> logger) =>
            _logger = logger;

        public void LogInfo(string message) =>
            _logger.LogInformation($"INFO {DateTime.Now.ToString()} | {typeof(TService).Name} : {message}");
        public void LogWarn(string message) =>
            _logger.LogWarning($"WARN {DateTime.Now.ToString()} | {typeof(TService).Name} : {message}");
        public void LogError(string message) =>
            _logger.LogError($"ERROR {DateTime.Now.ToString()} | {typeof(TService).Name} : {message}");
    }
}
