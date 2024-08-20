namespace FileProcess.Api.Contracts.Services
{
    public interface ILoggerService<TService> where TService : class
    {
        void LogError(string message);
        void LogInfo(string message);
        void LogWarn(string message);
    }
}