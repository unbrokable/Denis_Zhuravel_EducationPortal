using AutoMapper;
using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using Application.Interfaces.IServices;
using Domain.Entities;

namespace Application.Services
{
    public class CourseService : IServiceCourse
    {
        private readonly IEntitiesRepository repository;
        private readonly IServiceEntities<MaterialDTO> serviceMaterial;
        private readonly IAutoMapperBLConfiguration mapper;
        private readonly IServiceSkill serviceSkill;

        public CourseService(IEntitiesRepository repository, IServiceEntities<MaterialDTO> serviceMaterial,
            IAutoMapperBLConfiguration mapper, IServiceSkill serviceSkill)
        {
            this.repository = repository;
            this.serviceMaterial = serviceMaterial;
            this.mapper = mapper;
            this.serviceSkill = serviceSkill;
        }

        public void Create(CourseDTO data)
        {
            var course = mapper.GetMapper().Map<Course>(data);
            course.MaterialsId = course.MaterialsId
                .Where(i => serviceMaterial.GetAll().Select(j => j.Id).Contains(i))
                .Distinct()
                .ToList();
            course.SkillsId = course.SkillsId
                .Where(i => serviceSkill.GetAll().Select(j => j.Id).Contains(i))
                .Distinct()
                .ToList();

            if (!course.MaterialsId.Any() && !course.SkillsId.Any())
            {
                throw new ArgumentException("Too short");
            }
            foreach (var id in course.MaterialsId)
            {
                repository.Create<CompositionCourseMaterial>(
                    new CompositionCourseMaterial
                    {
                        CourseId = course.Id,
                        MaterialId = id
                    });
            }
            foreach (var idSkill in course.SkillsId)
            {
                repository.Create<CompositionSkillCourse>(new CompositionSkillCourse()
                {
                    CourseId = course.Id,
                    SkillId = idSkill,
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
                course.Skills = serviceSkill.GetAllSkillsOfCourse(course.Id).ToList();
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
            var course = mapper.GetMapper()
                .Map<Course, CourseDTO>(repository.GetAll<Course>()
                .FirstOrDefault(i => i.Id == id));
           
            if(course == null)
            {
                return null;
            }

            var materials = repository.GetAll<CompositionCourseMaterial>()
                       .Where(i => i.CourseId == course.Id).Select(i => i.MaterialId).ToList();
            course.Materials = serviceMaterial.GetAllBy(i => materials.Contains(i.Id)).ToList();
            course.Skills = serviceSkill.GetAllSkillsOfCourse(course.Id).ToList();
            return course;
        }

        public IEnumerable<CourseDTO> GetAllExceptChoosen(int userId)
        {
            return GetAllBy(j => !repository.GetAllBy<CompositionPassedMaterial>(i => i.UserId == userId)?
                .Select(i => i.CourseId)?.Contains(j.Id)??true); 
        }
    }
}
