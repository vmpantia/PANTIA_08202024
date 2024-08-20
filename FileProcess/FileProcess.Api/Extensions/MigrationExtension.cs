using FileProcess.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FileProcess.Api.Extensions
{
    public static class MigrationExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            using FileProcessDbContext context = scope.ServiceProvider.GetRequiredService<FileProcessDbContext>();
            context.Database.Migrate();
        }
    }
}
