using FileProcess.Api.Models.Entities;

namespace FileProcess.Api.Contracts.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
    }
}