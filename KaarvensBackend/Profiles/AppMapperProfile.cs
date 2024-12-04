using AutoMapper;
using KaarvensBackend.DTOS;

namespace KaarvensBackend.Profiles
{
    public class AppMapperProfile:Profile
    {
        /// <summary>
        /// Automapper is used for mapping the project details and drawing details tables (primary and forieng key)
        /// </summary>
        public AppMapperProfile()
        {
            //for post method
            CreateMap<ProjectDetailsDto, ProjectDetails>();
            CreateMap<DrawingDetailsDto, DrawingDetails>();
        }
    }
}
