using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Domain;
using Application.DTO.MaterialDTOs;
using Domain.Entities;
using System;
using Application.DTO.CheckDTOs;
using System.Collections;
using System.Collections.Generic;

namespace Application
{
    public class AutoMapperBLConfiguration : IAutoMapperBLConfiguration
    {
        private IMapper mapper = new MapperConfiguration(
           i =>
           {
               i.AddMaps(new[] {
                "Application"
                });

               i.CreateMap<CourseDTO, Course>();
               i.CreateMap<Course, CourseDTO>();

               i.CreateMap<User, UserDTO>();
               i.CreateMap<UserDTO, User>();

               i.CreateMap<Material, MateriaCheckDTO>()
               .ForMember(i => i.CourseAmount, j => j.MapFrom(j => j.Courses.Count))
               .ForMember(i => i.UsersAmount, j => j.MapFrom(j => j.Users.Count));

               i.CreateMap<Material, int>()
               .ConvertUsing(i => i.Id);

               i.CreateMap<CompositionPassedMaterial, int>()
               .ConvertUsing(i => i.MaterialId);

               i.CreateMap<CompositionPassedCourse, int>()
               .ConvertUsing(i => i.CourseId);

               
           }).CreateMapper();


        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
