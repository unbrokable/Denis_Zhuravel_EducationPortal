using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Domain;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services
{
    public class CoursePassingService : ICoursePassingService
    {
        private readonly IEntitiesRepository repository;
        private readonly IServiceEntities<MaterialDTO> serviceMaterial;
        private readonly IServiceCourse serviceCourse;
        private readonly IServiceUser serviceUser;

        public CoursePassingService(IEntitiesRepository repository, IServiceEntities<MaterialDTO> serviceMaterial, IServiceCourse serviceCourse,
           IServiceUser serviceUser)
        {
            this.serviceUser = serviceUser;
            this.serviceCourse = serviceCourse;
            this.repository = repository;
            this.serviceMaterial = serviceMaterial;
        }

        public bool ChooseCourse(int idUser, int idCourse)
        {
            if (serviceUser.GetById(idUser) == null || serviceCourse.GetById(idCourse) == null)
            {
                return false;
            }
            return repository.Create<CompositionPassedCourse>(new CompositionPassedCourse
            {
                CourseId = idCourse,
                UserId = idUser
            });
        }

        public IEnumerable<CourseDTO> GetCourses(Predicate<CourseDTO> predicate)
        {
            return serviceCourse.GetAllBy(i => predicate(i));
        }

        public CourseProgressDTO GetProgressCourse(int idUser, int idCourse)
        {
            if (serviceUser.GetById(idUser) == null
                || serviceCourse.GetById(idCourse) == null
                || repository.GetBy<CompositionPassedCourse>(i => i.CourseId == idCourse && i.UserId == idUser) == null)
            {
                return null;
            }

            var passedMaterialsId = repository
                .GetAllBy<CompositionPassedMaterial>(i => i.UserId == idUser)
                .Select(i => i.MaterialId)
                .Aggregate(new List<int>(), (i, j) => i.Union(new List<int>() { j }).ToList());

            var course = serviceCourse.GetById(idCourse);
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

        public IEnumerable<CourseProgressDTO> GetProgressCourses(int idUser)
        {
            var coursesId = repository
                .GetAllBy<CompositionPassedCourse>(i => i.UserId == idUser)
                .Select(i => i.CourseId)
                .ToList();
            List<CourseProgressDTO> courseProgresses = new List<CourseProgressDTO>();
            for (int i = 0; i < coursesId.Count; i++)
            {
                courseProgresses.Add(GetProgressCourse(idUser, coursesId[i]));
            }
            return courseProgresses;
        }

        public MaterialDTO PassMaterial(int idUser, int idCourse, int idMaterial)
        {
            var compositionPassedMaterial = repository.GetBy<CompositionPassedMaterial>(i => i.UserId == idUser && i.MaterialId == idMaterial);
            var course = serviceCourse.GetById(idCourse);

           
            if (compositionPassedMaterial != null)
            {
                return serviceMaterial.GetById(idMaterial);
            }
            else if (course == null || !course.Materials.Select(i => i.Id).Contains(idMaterial))
            {
                return null;
            }

            // add skills for user
            var user = repository.GetBy<User>(i => i.Id == idUser,i => i.Include(i=> i.PassedMaterials).Include(i => i.Skills));
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
            repository.Update(user);
            return serviceMaterial.GetById(idMaterial);
        }

    }
}
