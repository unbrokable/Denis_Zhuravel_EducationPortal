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
using Application.Interfaces.IServices;

namespace EducationPortal.Controllers
{
    class CourseManager:ICourseManager
    {
        private readonly IServiceCourse service;
        private readonly IAutoMapperUlConfiguration mapper;
        private readonly ISkillManager skillManager;

        public CourseManager(IServiceCourse service, IAutoMapperUlConfiguration mapper, ISkillManager skillManager)
        {
            this.skillManager = skillManager;
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
            Console.WriteLine("Write id of skills (1,2,3,4)");
            string skills = Console.ReadLine();
            CourseDTO course = new CourseDTO()
            {
                Name = name,
                Description = description,
                MaterialsId = materials.Split(',').Select(i => int.Parse(i)).ToList(),
                SkillsId = skills.Split(',').Select(i => int.Parse(i)).ToList(),
                UserId = userId
            };

            var validator = new CourseValidator();
            var validateResult = validator.Validate(course);
            if (!validateResult.IsValid)
            {
                Console.WriteLine(validateResult.ToString(","));
                return;
            }
            try
            {
                service.Create(course);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Course must contain at least one skill and one material");
            }
            
        }

        public void ShowCreatedCourseByUser(int userId)
        {
            var courses = service.GetAllBy(i => i.UserId == userId);
            var coursesview = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                coursesview.Add(mapper.GetMapper().Map<CourseDTO, CourseViewModel>(course));
            }
            foreach (var item in coursesview)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void ShowCourses(int idUser)
        {
            var courses = service.GetAllExceptChoosen(idUser);
            var coursesview = new List<CourseViewModel>();
            foreach (var course in courses)
            {
                coursesview.Add(mapper.GetMapper().Map<CourseDTO, CourseViewModel>(course));
            }
            foreach (var item in coursesview)
            {
                Console.WriteLine($"Course id {item.Id} Name {item.Name}\nDescription {item.Description}\n");
            }
        }

        public void ShowSkills()
        {
            skillManager.Show();
        }

        public void CreateSkill()
        {
            skillManager.Create();
        }

    }
}
