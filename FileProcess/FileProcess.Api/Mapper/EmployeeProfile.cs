using AutoMapper;
using FileProcess.Api.Models.Entities;

namespace FileProcess.Api.Mapper
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, Employee>()
                .ForMember(dst => dst.Id, opt => opt.Ignore())
                .ForMember(dst => dst.CreatedAt, opt => opt.Ignore());
        }
    }
}
