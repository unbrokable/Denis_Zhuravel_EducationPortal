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
using System.Threading.Tasks;

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

        public async Task CreateCourseAsync(int userId)
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
               await  service.CreateAsync(course);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Course must contain at least one skill and one material");
            }
            
        }

        public async Task ShowCreatedCourseByUserAsync(int userId)
        {
            var coursesview = mapper
                .GetMapper()
                .Map<IEnumerable<CourseDTO>,IEnumerable<CourseViewModel>>(await service.GetCourseOfCreatorAsync(userId));
            foreach (var item in coursesview)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public async Task ShowCoursesAsync(int idUser)
        {
            var coursesview = mapper
                .GetMapper()
                .Map<IEnumerable<CourseDTO>, IEnumerable<CourseViewModel>>( await service.GetAllExceptChoosenAsync(idUser));
            foreach (var item in coursesview)
            {
                Console.WriteLine($"Course id {item.Id} Name {item.Name}\nDescription {item.Description}\n");
            }
        }

        public async Task ShowSkillsAsync()
        {
            await skillManager.ShowAsync();
        }

        public async Task CreateSkillAsync()
        {
            await skillManager.CreateAsync();
        }

        public async Task Remove(int idUser)
        {
            await ShowCreatedCourseByUserAsync(idUser);
            Console.WriteLine("Enter id of Course");
            try
            {
                await service.Remove(Convert.ToInt32(Console.ReadLine()));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid data");
            }
        }
    }
}
