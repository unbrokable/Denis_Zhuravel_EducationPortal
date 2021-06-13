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
using Microsoft.EntityFrameworkCore;

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
            course.Materials = repository.GetAllBy<Material>(i => data.MaterialsId.Contains(i.Id)).ToList();
            course.Skills = repository.GetAllBy<Skill>(i => data.SkillsId.Contains(i.Id)).ToList();
            if (!course.Materials.Any() && !course.Skills.Any())
            {
                throw new ArgumentException("Too short");
            }
            repository.Create(course);
        }

        public CourseDTO GetBy(Predicate<CourseDTO> predicate )
        {
            var course = repository.GetBy<Course>(i => PredicateTranform(i, predicate), i => i.Include(i => i.Materials).Include(i => i.Skills));
            return mapper.GetMapper()
                .Map<CourseDTO>(course);
        }

        public IEnumerable<CourseDTO> GetAllBy(Predicate<CourseDTO> predicate)
        {
            return mapper.GetMapper()
                .Map<IEnumerable<CourseDTO>>(repository.GetAllBy<Course>(i => PredicateTranform(i, predicate), i => i.Include(i => i.Materials).Include(i => i.Skills)));
        }

        public CourseDTO GetById(int id)
        {
            var course = GetBy(i => i.Id == id);
            if (course == null)
            {
                return null;
            }
            return course;
        }

        public IEnumerable<CourseDTO> GetAllExceptChoosen(int userId)
        {
            return GetAllBy(j => !repository.GetAllBy<CompositionPassedCourse>(i => i.UserId == userId)?
                .Select(i => i.CourseId)?.Contains(j.Id)??true); 
        }

        bool PredicateTranform(Course course, Predicate<CourseDTO> predicate)
        {
            return predicate(mapper.GetMapper().Map<Course, CourseDTO>(course));
        }

    }
}
