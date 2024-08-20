using FileProcess.Api.Contracts.Repositories;
using FileProcess.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FileProcess.Api.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(FileProcessDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetEmployees() =>
            await GetAll().ToListAsync();
    }
}
