using Application.DTO.MaterialDTOs;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Infrastructure.Profiles
{
    public class MaterialProfile:Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialDTO, Material>()
              .Include<VideoMaterialDTO, VideoMaterial>()
              .Include<BookMaterialDTO, BookMaterial>()
              .Include<ArticleMaterialDTO, ArticleMaterial>();

            CreateMap<Material, MaterialDTO>()
             .ForMember(i => i.Creator, i => i.Ignore())
            .Include<VideoMaterial, VideoMaterialDTO>()
            .Include<BookMaterial, BookMaterialDTO>()
            .Include<ArticleMaterial, ArticleMaterialDTO>();

            CreateMap<BookMaterialDTO, BookMaterial>();
            CreateMap<ArticleMaterialDTO, ArticleMaterial>();

            CreateMap<BookMaterial, BookMaterialDTO>();
            CreateMap<ArticleMaterial, ArticleMaterialDTO>();

        }
    }
}
