using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Domain;
using Application.DTO.MaterialDTOs;
using Domain.Entities;
using System;

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

           }).CreateMapper();


        public IMapper GetMapper()
        {
            return mapper;
        }
    }
}
