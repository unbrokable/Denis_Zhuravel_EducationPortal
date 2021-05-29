using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Domain;
using Application.DTO.MaterialDTOs;

namespace Application
{
    public class AutoMapperBLConfiguration: IAutoMapperBLConfiguration
    {
        private IMapper mapper = new MapperConfiguration(
           i =>
           {
               i.CreateMap<MaterialDTO, Material>()
              .Include<VideoMaterialDTO, VideoMaterial>()
              .Include<BookMaterialDTO, BookMaterial>()
              .Include<ArticleMaterialDTO, ArticleMaterial>();
               i.CreateMap<ResolutionDTO, Resolution>();

               i.CreateMap<Resolution, ResolutionDTO>();
               i.CreateMap<Material, MaterialDTO>()
                .ForMember(i => i.Creator, i => i.Ignore())
               .Include<VideoMaterial, VideoMaterialDTO>()
               .Include<BookMaterial, BookMaterialDTO>()
               .Include<ArticleMaterial, ArticleMaterialDTO>();

               i.CreateMap<VideoMaterialDTO, VideoMaterial>();
               i.CreateMap<BookMaterialDTO, BookMaterial>();
               i.CreateMap<ArticleMaterialDTO, ArticleMaterial>();
               i.CreateMap<ResolutionDTO, Resolution>();

               i.CreateMap<VideoMaterial, VideoMaterialDTO>();
               i.CreateMap<BookMaterial, BookMaterialDTO>();
               i.CreateMap<ArticleMaterial, ArticleMaterialDTO>();
               i.CreateMap<Resolution, ResolutionDTO>();

               i.CreateMap<CourseDTO, Course>();
               i.CreateMap<Course, CourseDTO>();

               i.CreateMap<User, UserDTO>();
               i.CreateMap<UserDTO, User>();
           }).CreateMapper();


        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
