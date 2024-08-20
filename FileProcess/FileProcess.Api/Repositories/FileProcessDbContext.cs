using FileProcess.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileProcess.Api.Repositories
{
    public class FileProcessDbContext : DbContext
    {
        public FileProcessDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Models.Entities.File> Files { get; set; }
    }
}
