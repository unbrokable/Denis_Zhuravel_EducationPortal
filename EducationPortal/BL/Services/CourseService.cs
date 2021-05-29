using AutoMapper;
using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class CourseService : IServiceEntities<CourseDTO>
    {
        private readonly IEntitiesRepository repository;
        private readonly IServiceEntities<MaterialDTO> serviceMaterial;
        private readonly IAutoMapperBLConfiguration mapper;
       
        public CourseService(IEntitiesRepository repository, IServiceEntities<MaterialDTO> serviceMaterial, IAutoMapperBLConfiguration mapper)
        {
            this.repository = repository;
            this.serviceMaterial = serviceMaterial;
            this.mapper = mapper;
        }

        public void Create(CourseDTO data)
        {
            var course = mapper.GetMapper().Map<Course>(data);
            foreach (var id in course.MaterialsId)
            {
                repository.Create<CompositionCourseMaterial>(
                    new CompositionCourseMaterial
                    {
                        CourseId = course.Id,
                        MaterialId = id
                    });
            }
            repository.Create(course);
        }
            
        public IEnumerable<CourseDTO> GetAll()
        {
            var courses = mapper.GetMapper().Map<IEnumerable<CourseDTO>>(repository.GetAll<Course>());
            foreach (var course in courses)
            {
                var materials = repository.GetAll<CompositionCourseMaterial>()
                    .Where(i => i.CourseId == course.Id).Select(i => i.MaterialId).ToList();
                course.Materials = serviceMaterial.GetAllBy(i => materials.Contains(i.Id)).ToList();
            }
            return courses;
        }

        public CourseDTO GetBy(Predicate<CourseDTO> predicate)
        {
            return mapper.GetMapper()
                .Map<IEnumerable<CourseDTO>>(repository.GetAll<Course>())
                .FirstOrDefault(i => predicate(i));
        }

        public IEnumerable<CourseDTO> GetAllBy(Predicate<CourseDTO> predicate)
        {
            return mapper.GetMapper()
                .Map<IEnumerable<CourseDTO>>(repository.GetAll<Course>())
                .Where(i => predicate(i));
        }

        public CourseDTO GetById(int id)
        {
            return mapper.GetMapper()
                .Map<Course, CourseDTO>(repository.GetAll<Course>()
                .FirstOrDefault(i => i.Id == id));
        }
    }
}
