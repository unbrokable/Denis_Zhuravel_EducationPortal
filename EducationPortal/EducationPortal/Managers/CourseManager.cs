using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using System.Linq;
using EducationPortal.Models;
using System.Collections.Generic;
using System;
using AutoMapper;
using EducationPortal.Interfaces;
using EducationPortal.Models.Validators;

namespace EducationPortal.Controllers
{
    class CourseManager:ICourseManager
    {
        readonly IServiceEntities<CourseDTO> service;
        readonly IAutoMapperUlConfiguration mapper;
        public CourseManager( IServiceEntities<CourseDTO> service, IAutoMapperUlConfiguration mapper )
        {       
            this.service = service;
            this.mapper = mapper; 
        }

        public void CreateCourse(int userId)
        {
            Console.WriteLine("___________Creating course_____________");
            Console.WriteLine("name");
            string name = Console.ReadLine();
            Console.WriteLine("Description");
            string description = Console.ReadLine();
            Console.WriteLine("Write id of material (1,2,3,4)");
            string materials = Console.ReadLine();
            CourseDTO course = new CourseDTO()
            {
                Name = name,
                Description = description,
                MaterialsId = materials.Split(',').Select(i => int.Parse(i)).ToList(),
                Id = new Random().Next(0,1000),
                UserId = userId
            };

            var validator = new CourseValidator();
            var validateResult = validator.Validate(course);
            if (!validateResult.IsValid)
            {
                Console.WriteLine(validateResult.ToString(","));
                return;
            }

            service.Create(course);
        }

        public void ShowCreatedCourseByUser(int userId)
        {
            var courses = service.GetAll().Where(i => i.UserId == userId);
            var coursesview = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                coursesview.Add(mapper.CreateMapper().Map<CourseDTO, CourseViewModel>(course));
            }
            foreach (var item in coursesview)
            {
                Console.WriteLine(item.ToString());
            }
        }

    }
}
