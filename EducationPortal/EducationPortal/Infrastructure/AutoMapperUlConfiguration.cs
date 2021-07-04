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
                i.AddMaps(new[] {
                    "EducationPortal"
                });
            }).CreateMapper();


        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
