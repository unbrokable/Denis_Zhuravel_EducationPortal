using Application.DTO.MaterialDTOs;
using AutoMapper;
using EducationPortal.Models;
using EducationPortal.Models.MaterialsViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Infrastructure.Profiles
{
    class MaterialProfile :Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialDTO, MaterialViewModel>()
              .Include<VideoMaterialDTO, VideoMaterialViewModel>()
              .Include<BookMaterialDTO, BookMaterialViewModel>()
              .Include<ArticleMaterialDTO, ArticleMaterialViewModel>();
            CreateMap<ResolutionDTO, ResolutionViewModel>();

            CreateMap<ResolutionViewModel, ResolutionDTO>();
            CreateMap<MaterialViewModel, MaterialDTO>()
             .ForMember(i => i.Creator, i => i.Ignore())
            .Include<VideoMaterialViewModel, VideoMaterialDTO>()
            .Include<BookMaterialViewModel, BookMaterialDTO>()
            .Include<ArticleMaterialViewModel, ArticleMaterialDTO>();

            CreateMap<VideoMaterialDTO, VideoMaterialViewModel>();
            CreateMap<BookMaterialDTO, BookMaterialViewModel>();
            CreateMap<ArticleMaterialDTO, ArticleMaterialViewModel>();

            CreateMap<VideoMaterialViewModel, VideoMaterialDTO>();
            CreateMap<BookMaterialViewModel, BookMaterialDTO>();
            CreateMap<ArticleMaterialViewModel, ArticleMaterialDTO>();
        }
    }
}
