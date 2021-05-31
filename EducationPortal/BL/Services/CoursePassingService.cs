using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
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
        private readonly IServiceEntities<CourseDTO> serviceCourse;
        private readonly IServiceUser serviceUser;

       // private readonly IAutoMapperBLConfiguration mapper;

        public CoursePassingService(IEntitiesRepository repository, IServiceEntities<MaterialDTO> serviceMaterial,IServiceEntities<CourseDTO> serviceCourse,
           IServiceUser serviceUser )
        {
            this.serviceUser = serviceUser;
            this.serviceCourse = serviceCourse; 
            this.repository = repository;
            this.serviceMaterial = serviceMaterial;
            //this.mapper = mapper;
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
                MaterialsId = new List<int>()
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

            var course = serviceCourse.GetById( idCourse);
            CourseProgressDTO courseProgress = new CourseProgressDTO()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                MaterialsPassed = course.Materials.Where(i => passedMaterialsId.Contains(i.Id)).ToList(),
                MaterialsNotPassed = course.Materials.Where(i => !passedMaterialsId.Contains(i.Id)).ToList()
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
           // var compositionPassedMaterials = repository.GetAll<CompositionPassedMaterial>();
            var compositionPassedMaterial = repository.GetBy<CompositionPassedMaterial>(i => i.CourseId == idCourse && i.UserId == idUser);
            var course = serviceCourse.GetById(idCourse);

            if(compositionPassedMaterial == null)
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
            compositionPassedMaterial.MaterialsId.Add(idMaterial);

            repository.Update<CompositionPassedMaterial>(compositionPassedMaterial, i => compositionPassedMaterial.CourseId == i.CourseId 
                && compositionPassedMaterial.UserId == i.UserId); 

            return serviceMaterial.GetById(idMaterial);
        }
    }
}
