﻿using Application.DTO;
using Application.DTO.MaterialDTOs;
using Application.Interfaces;
using EducationPortal.Interfaces;
using EducationPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationPortal.Managers
{
    class PassCourseManager: IPassCourseManager
    {
        private readonly ICourseManager courseManager;
        private readonly ICoursePassingService passingService;
        private readonly IAutoMapperUlConfiguration mapper;
       
        public PassCourseManager(ICoursePassingService passingService, ICourseManager courseManager, IAutoMapperUlConfiguration mapper)
        {
            this.mapper = mapper;
            this.passingService = passingService;
            this.courseManager = courseManager;
        }

        public void ChooseCourse(int idUser)
        {
            courseManager.ShowCourses(idUser);
            Console.WriteLine("Choose id course");
            string answer = Console.ReadLine();
            try
            {
               if(!passingService.ChooseCourse(idUser, int.Parse(answer)))
                {
                    Console.WriteLine("Cant find course with this id");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid data");
            }
        }

        public void ChooseCourseToPass(int idUser)
        {
           var courses = mapper.GetMapper()
                .Map<IEnumerable<CourseProgressDTO>,IEnumerable<CourseProgressViewModel> >(passingService.GetProgressCourses(idUser));
            foreach (var course  in courses)
            {
                if(course.MaterialsNotPassed.Any())
                {
                    Console.WriteLine(course.ToString());
                }
                else
                {
                    var color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(course.ToString());
                    Console.ForegroundColor = color;
                }
                
            }
            Console.WriteLine("Enter id  of course");
            string answer = Console.ReadLine();
            try
            {
                PassCourse(idUser, int.Parse(answer));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid data");
            }
        }

        public void PassCourse(int idUser, int idCourse)
        {
            while (true)
            {
                var course = mapper.GetMapper()
                    .Map<CourseProgressDTO, CourseProgressViewModel>(passingService.GetProgressCourse(idUser, idCourse));
                Console.WriteLine(course.ToString());
                Console.WriteLine("Enter id of material (-1 to exit)");
                string answer = Console.ReadLine();
                if (answer.Equals("-1")) return;
                var material = mapper.GetMapper()
                    .Map<MaterialDTO, MaterialViewModel>(passingService.PassMaterial(idUser, idCourse, int.Parse(answer)));

                if (material == null) { 
                    Console.WriteLine("Invalid id of material");
                }
                else
                {
                    Console.WriteLine(material.ToString());
                }

            }
        }
    }
}