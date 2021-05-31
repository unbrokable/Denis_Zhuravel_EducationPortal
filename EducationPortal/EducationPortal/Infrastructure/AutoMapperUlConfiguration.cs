using AutoMapper;
using Application.DTO;
using Application.DTO.MaterialDTOs;
using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using EducationPortal.Interfaces;
using EducationPortal.Models.MaterialsViewModels;

namespace EducationPortal
{
    class AutoMapperUlConfiguration : IAutoMapperUlConfiguration
    {
        private IMapper mapper = new MapperConfiguration(
            i =>
            {
                i.CreateMap<MaterialDTO, MaterialViewModel>()
               .Include<VideoMaterialDTO, VideoMaterialViewModel>()
               .Include<BookMaterialDTO, BookMaterialViewModel>()
               .Include<ArticleMaterialDTO, ArticleMaterialViewModel>();
                i.CreateMap<ResolutionDTO, ResolutionViewModel>();

                i.CreateMap<ResolutionViewModel, ResolutionDTO>();
                i.CreateMap<MaterialViewModel, MaterialDTO>()
                 .ForMember(i => i.Creator, i => i.Ignore())
                .Include<VideoMaterialViewModel, VideoMaterialDTO>()
                .Include<BookMaterialViewModel, BookMaterialDTO>()
                .Include<ArticleMaterialViewModel, ArticleMaterialDTO>();

                i.CreateMap<VideoMaterialDTO, VideoMaterialViewModel>();
                i.CreateMap<BookMaterialDTO, BookMaterialViewModel>();
                i.CreateMap<ArticleMaterialDTO, ArticleMaterialViewModel>();

                i.CreateMap<VideoMaterialViewModel, VideoMaterialDTO>();
                i.CreateMap<BookMaterialViewModel, BookMaterialDTO>();
                i.CreateMap<ArticleMaterialViewModel, ArticleMaterialDTO>();

                i.CreateMap<CourseDTO, CourseViewModel>();
                i.CreateMap<CourseViewModel, CourseDTO>();

                i.CreateMap<CourseProgressDTO, CourseProgressViewModel>();
                i.CreateMap<CourseProgressViewModel, CourseProgressDTO>();

            }).CreateMapper();


        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
