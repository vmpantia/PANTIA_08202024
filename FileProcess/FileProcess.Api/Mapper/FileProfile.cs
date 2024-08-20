using AutoMapper;
using FileProcess.Api.Models.Dtos;

namespace FileProcess.Api.Mapper
{
    public class FileProfile : Profile
    {
        public FileProfile()
        {
            CreateMap<Models.Entities.File, FileDto>()
                .ForMember(dst => dst.ProcessingTimeInSeconds, opt => opt.MapFrom(src => (src.FinishedAt - src.StartAt).TotalSeconds));
        }
    }
}
