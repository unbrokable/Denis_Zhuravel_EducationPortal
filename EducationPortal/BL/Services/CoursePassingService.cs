﻿using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Specification;
using Domain;
using Domain.Entities;
using Domain.Specification;
using Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CoursePassingService : ICoursePassingService
    {
        private readonly IEntitiesRepository repository;
        private readonly IServiceMaterial serviceMaterial;
        private readonly IServiceCourse serviceCourse;
        private readonly IServiceUser serviceUser;
        private readonly IAutoMapperBLConfiguration mapper;

        public CoursePassingService(IEntitiesRepository repository, IServiceMaterial serviceMaterial, IServiceCourse serviceCourse,
           IServiceUser serviceUser,IAutoMapperBLConfiguration mapper)
        {
            this.serviceUser = serviceUser;
            this.serviceCourse = serviceCourse;
            this.repository = repository;
            this.serviceMaterial = serviceMaterial;
            this.mapper = mapper;
        }

        public async Task<bool> ChooseCourseAsync(int idUser, int idCourse)
        {
            if ( await serviceUser.GetByIdAsync(idUser) == null || await serviceCourse.GetByIdAsync(idCourse) == null)
            {
                return false;
            }
            return  await repository.AddAsync<CompositionPassedCourse>(new CompositionPassedCourse
            {
                CourseId = idCourse,
                UserId = idUser
            });
        }

        public async Task<CourseProgressDTO> GetProgressCourseAsync(int idUser, int idCourse)
        {

            var passedMaterialsId = (await repository.GetCustomSelectAsync<CompositionPassedMaterial, int>(PassedMaterialSpecification.FilterByUserId(idUser),
                mapper.GetMapper().ConfigurationProvider))
                .Distinct()
                .ToList();

            var course = (await serviceCourse.GetByIdAsync(idCourse));
            if(course == null)
            {
                return null;
            }
            CourseProgressDTO courseProgress = new CourseProgressDTO()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                MaterialsPassed = course.Materials.Where(i => passedMaterialsId.Contains(i.Id)).ToList(),
                MaterialsNotPassed = course.Materials.Where(i => !passedMaterialsId.Contains(i.Id)).ToList(),
                Skills = course.Skills
            };
            return courseProgress;
        }

        public async Task<IEnumerable<CourseProgressDTO>> GetProgressCoursesAsync(int idUser)
        {
            var coursesId = (await repository
                .GetAsync<CompositionPassedCourse>(PassedCourseSpecification.FilterByUserId(idUser)))
                .Select(i => i.CourseId)
                .ToList();
            List<CourseProgressDTO> courseProgresses = new List<CourseProgressDTO>();
            for (int i = 0; i < coursesId.Count; i++)
            {
                courseProgresses.Add( await GetProgressCourseAsync(idUser, coursesId[i]));
            }
            return courseProgresses;
        }

        public async Task<MaterialDTO> PassMaterialAsync(int idUser, int idCourse, int idMaterial)
        {
            var compositionPassedMaterial =  await repository
                .FindAsync<CompositionPassedMaterial>(PassedMaterialSpecification.FilterByUserId(idUser) 
                 .And(PassedMaterialSpecification.FilterByMaterialId(idMaterial) ));
            var course = await serviceCourse.GetByIdAsync(idCourse);
           
            if (compositionPassedMaterial != null)
            {
                return await serviceMaterial.GetByIdAsync(idMaterial);
            }
            else if (course == null || !course.Materials.Select(i => i.Id).Contains(idMaterial))
            {
                return null;
            }
            
            var user =  await repository.FindAsync<User>(UserSpecification.FilterById(idUser),i=> i.PassedMaterials, i => i.Skills);
            var passedMaterialOfCourse = user.PassedMaterials
                .Select(i => i.MaterialId)
                .Intersect(course.Materials.Select(i => i.Id));
                
            if (course.Materials.Count == passedMaterialOfCourse.Count() + 1)
            {
                foreach (var skill in course.Skills)
                {
                    if (user.Skills.Select(i => i.SkillId).Contains(skill.Id))
                    {
                        user.Skills.FirstOrDefault(i => i.SkillId == skill.Id).Level++;
                    }
                    else
                    {
                        user.Skills.Add(new CompositionSkillUser() { SkillId = skill.Id, Level = 1 });
                    }
                }
            }
           user.PassedMaterials.Add(new CompositionPassedMaterial() { MaterialId = idMaterial, UserId = idUser });
            await repository.UpdateAsync(user);
            return await serviceMaterial.GetByIdAsync(idMaterial);
        }

    }
}
