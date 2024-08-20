using FileProcess.Api.Contracts.Repositories;
using FileProcess.Api.Contracts.Services;
using FileProcess.Api.Repositories;
using FileProcess.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FileProcess.Api.Extensions
{
    public static class ServiceExtension
    {
        public static void AddDatabases(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<FileProcessDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("MigrationDb")));

        public static void AddRepositories(this IServiceCollection services) =>
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
                    .AddScoped<IFileRepository, FileRepository>()
                    .AddScoped<IEmployeeRepository, EmployeeRepository>();

        public static void AddServices(this IServiceCollection services) =>
            services.AddScoped<IAuthenticationService, AuthenticationService>()
                    .AddScoped<IFileService, FileService>()
                    .AddScoped(typeof(ISynchronizationService<,>), typeof(SynchronizationService<,>))
                    .AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

        public static void AddMapperProfiles(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
