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
using Domain.Specification;
using Application.Specification;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CourseService : IServiceCourse
    {
        private readonly IEntitiesRepository repository;
        private readonly IAutoMapperBLConfiguration mapper;
        private readonly ILogger logger;

        public CourseService(IEntitiesRepository repository,
            IAutoMapperBLConfiguration mapper, ILogger logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task CreateAsync(CourseDTO data)
        {
            //change
            var course = mapper.GetMapper().Map<Course>(data);
            course.Materials = (await repository.GetAsync<Material>(new Specification<Material>( i => data.MaterialsId.Contains(i.Id)))).ToList();
            course.Skills = (await repository.GetAsync<Skill>(new Specification<Skill>(i => data.SkillsId.Contains(i.Id)))).ToList();
            if (!course.Materials.Any() || !course.Skills.Any())
            {
                logger.LogWarning($"Course dont enought have skills {course.Skills.Any()} or materials {course.Materials.Any()}");
                throw new ArgumentException("Too short");
            }
            await repository.AddAsync(course);
        }

        public async Task<CourseDTO> GetByIdAsync(int id)
        {
            var course =  await repository.FindAsync<Course>(CourseSpecification.FilterById(id), i => i.Materials,i => i.Skills);
            if (course == null)
            {
                return null;
            }
            return mapper.GetMapper()
                .Map<CourseDTO>(course);;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllExceptChoosenAsync(int userId)
        {
           return mapper.GetMapper().Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(await repository.GetAsync<Course>(CourseSpecification.FilterByNotChoosenByUser(userId),i => i.Materials, i => i.Skills)).ToList(); 
        }

        public async Task<IEnumerable<CourseDTO>> GetCourseOfCreatorAsync(int userId)
        {
            return mapper.GetMapper().Map<IEnumerable<Course>,IEnumerable<CourseDTO>>( await repository.GetAsync<Course>(CourseSpecification.FilterByCreatorId(userId),i => i.Materials, i => i.Skills))
                .ToList();
           
        }

        //Change after mvc 
        public async Task<IEnumerable<CourseDTO>> GetAsync(int amount)
        {
           
            var result =(await repository.GetAsync<Course>(0,amount,null, i => i.Materials,i => i.Skills));
            // change on page after mvc
            return mapper.GetMapper().Map<IEnumerable<Course>, IEnumerable<CourseDTO>>(result.Items)
                .ToList();
        }

        public async Task Remove(int id)
        {
            var checkQuery = (await repository.FindAsync<CompositionPassedCourse>(PassedCourseSpecification.FilterByCourseId(id)));
            if (checkQuery != null)
            {
                throw new ArgumentException("This course is used and cant be deleted");
            }

            await repository.RemoveAsync<Course>(id);
        }
    }
}
