using AutoMapper;
using FileProcess.Api.Models.Dtos;
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
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dst => dst.LastUpdateAt, opt => opt.MapFrom(src => src.UpdatedAt ?? src.CreatedAt));
        }
    }
}
