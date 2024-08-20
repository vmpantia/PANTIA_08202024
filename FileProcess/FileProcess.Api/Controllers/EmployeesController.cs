using AutoMapper;
using FileProcess.Api.Contracts.Repositories;
using FileProcess.Api.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FileProcess.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesDtoAsync()
        {
            // Get all the employees imported in the database
            var files = await _employeeRepository.GetEmployeesAsync();

            // Convert entities to dto
            var result = _mapper.Map<IEnumerable<EmployeeDto>>(files);

            return Ok(result);
        }
    }
}
