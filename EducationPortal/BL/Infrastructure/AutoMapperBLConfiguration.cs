using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Domain;
using Application.DTO.MaterialDTOs;
using Domain.Entities;
using System;

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

               i.CreateMap<Material, MaterialDTO>()
                .ForMember(i => i.Creator, i => i.Ignore())
               .Include<VideoMaterial, VideoMaterialDTO>()
               .Include<BookMaterial, BookMaterialDTO>()
               .Include<ArticleMaterial, ArticleMaterialDTO>();

               i.CreateMap<VideoMaterialDTO, VideoMaterial>()
               .ForMember(i => i.Height, j => j.MapFrom(j => j.Resolution.Height))
               .ForMember(i => i.Width, j => j.MapFrom(j => j.Resolution.Width));

               i.CreateMap<BookMaterialDTO, BookMaterial>();
               i.CreateMap<ArticleMaterialDTO, ArticleMaterial>();

               i.CreateMap<VideoMaterial, VideoMaterialDTO>()
               .ForPath(i => i.Resolution.Height, j => j.MapFrom(j => j.Height))
               .ForPath(i => i.Resolution.Width, j => j.MapFrom(j => j.Width));
               i.CreateMap<BookMaterial, BookMaterialDTO>();
               i.CreateMap<ArticleMaterial, ArticleMaterialDTO>();

               i.CreateMap<CourseDTO, Course>();
               i.CreateMap<Course, CourseDTO>();

               i.CreateMap<User, UserDTO>();
               i.CreateMap<UserDTO, User>();

               i.CreateMap<SkillDTO, Skill>();
               i.CreateMap<Skill, SkillDTO>();

               i.CreateMap<Predicate<SkillDTO>, Predicate<Skill>>();
               i.CreateMap<Predicate<Skill>, Predicate<SkillDTO>>();

               i.CreateMap<CompositionSkillUser, SkillUserDTO>()
               .ForMember(i => i.Name, j => j.MapFrom(k => k.Skill.Name))
               .ForMember(i => i.Id, j => j.MapFrom(j => j.SkillId));
               
           }).CreateMapper();


        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
