using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Domain.Entities;
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
            return repository.Create<CompositionPassedMaterial>(new CompositionPassedMaterial
            {
                CourseId = idCourse,
                UserId = idUser,
                MaterialsId = repository.GetBy<CompositionPassedMaterial>(i => i.CourseId == idCourse && i.UserId == idUser )
                ?.MaterialsId?.ToList() ??new  List<int>()
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
                || repository.GetBy<CompositionPassedMaterial>(i => i.CourseId == idCourse && i.UserId == idUser) == null)
            {
                return null;
            }

            var passedMaterialsId = repository
                .GetAllBy<CompositionPassedMaterial>(i => i.UserId == idUser)
                .Select(i => i.MaterialsId)
                .Aggregate(new List<int>(), (i, j) => i.Union(j).ToList());

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
                .GetAllBy<CompositionPassedMaterial>(i => i.UserId == idUser)
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
            var compositionPassedMaterial = repository.GetBy<CompositionPassedMaterial>(i => i.CourseId == idCourse && i.UserId == idUser);
            var course = serviceCourse.GetById(idCourse);

            if (compositionPassedMaterial == null)
            {
                return null;
            }
            else if (compositionPassedMaterial.MaterialsId.Contains(idMaterial))
            {
                return serviceMaterial.GetById(idMaterial);
            }
            else if (course == null || !course.MaterialsId.Contains(idMaterial))
            {
                return null;
            }         
            // add skills for user
            if (course.MaterialsId.Count == compositionPassedMaterial.MaterialsId.Count+1)
            {
                //change after EF
                var usersSkills = repository.GetAll<CompositionSkillUser>()?.ToList() ?? new List<CompositionSkillUser>();
                foreach (var skill in course.Skills)
                {
                    var userSkill = usersSkills.FirstOrDefault(i => i.UserId == idUser && skill.Id == i.SkillId);
                    if (usersSkills.IndexOf(userSkill) != -1)
                    {
                        usersSkills[usersSkills.IndexOf(userSkill)].Level++;
                        repository.Update<CompositionSkillUser>(usersSkills[usersSkills.IndexOf(userSkill)], i => i.UserId == idUser && skill.Id == i.SkillId);
                    }
                    else
                    {
                        repository.Create<CompositionSkillUser>(
                            new CompositionSkillUser
                            {
                                UserId = idUser,
                                SkillId = skill.Id,
                                Level = 1
                            });
                    }
                }

            }
            compositionPassedMaterial.MaterialsId.Add(idMaterial);
            repository.Update<CompositionPassedMaterial>(compositionPassedMaterial, i => compositionPassedMaterial.CourseId == i.CourseId
                && compositionPassedMaterial.UserId == i.UserId);

            return serviceMaterial.GetById(idMaterial);
        }

    }
}
