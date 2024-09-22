using AutoMapper;
using KaarvensBackend.DTOS;

namespace KaarvensBackend.Profiles
{
    public class AppMapperProfile:Profile
    {
        public AppMapperProfile()
        {
            CreateMap<ProjectDetailsDto, ProjectDetails>();
            CreateMap<DrawingDetailsDto, DrawingDetails>();
        }
    }
}
