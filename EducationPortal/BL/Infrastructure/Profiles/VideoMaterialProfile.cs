using Application.DTO.MaterialDTOs;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Infrastructure.Profiles
{
    public class VideoMaterialProfile:Profile
    {
        public VideoMaterialProfile()
        {
            CreateMap<VideoMaterialDTO, VideoMaterial>()
               .ForMember(i => i.Height, j => j.MapFrom(j => j.Resolution.Height))
               .ForMember(i => i.Width, j => j.MapFrom(j => j.Resolution.Width));

            CreateMap<VideoMaterial, VideoMaterialDTO>()
           .ForPath(i => i.Resolution.Height, j => j.MapFrom(j => j.Height))
           .ForPath(i => i.Resolution.Width, j => j.MapFrom(j => j.Width));
        }
    }
}
