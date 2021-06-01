using EducationPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Controllers
{
    class MainManager: IManager
    {
        private readonly ICourseManager courseController;
        private readonly IAuthorizationManager authorizationController;
        private readonly IMaterialManager materialManager;
        private readonly IPassCourseManager passCourseManager;

        public MainManager(ICourseManager courseController, IAuthorizationManager authorizationController, IMaterialManager materialController, IPassCourseManager passCourseManager )
        {
            this.materialManager = materialController;
            this.authorizationController = authorizationController;
            this.courseController = courseController;
            this.passCourseManager = passCourseManager;
        }

        public void Start()
        {
            var curUser = authorizationController.Enter();
            Console.Clear();
            Console.WriteLine($"Hi {curUser.Id} {curUser.Name} \nYour email {curUser.Email} \n ");
            string answer;
            bool logout = false;
            while (!logout)
            {
                Console.WriteLine("Create Material\t1\nCreate Skills\t2\nCreate course\t3\n" +
                    "Courses by you\t4\nPass course\t5\nChoose course\t6\n" +
                    "About me\t7\nLog out\t8");
                answer = Console.ReadLine();
                Console.Clear();
                switch (answer)
                {
                    case "1":
                        try
                        {
                            materialManager.CreateMaterial(curUser.Id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid data for material");
                        }
                        Console.ReadKey();
                        break; 
                    case "2":
                        courseController.CreateSkill();
                        break;
                    case "3":
                        Console.WriteLine("Materials\n");
                        foreach (var item in materialManager.ShowAvaibleMaterial(curUser.Id))
                        {
                            Console.Write(item.ToString());
                        }
                        Console.WriteLine("Skills\n");
                        courseController.ShowSkills();
                        try
                        {
                            courseController.CreateCourse(curUser.Id);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid data for course");
                        }
                        Console.ReadKey();
                        break;
                    case "4":
                        courseController.ShowCreatedCourseByUser(curUser.Id);
                        Console.ReadKey();
                        break;
                    case "5":
                        passCourseManager.ChooseCourseToPass(curUser.Id);
                        break;
                    case "6":
                        passCourseManager.ChooseCourse(curUser.Id);
                        break;
                    case "7":
                        Console.WriteLine(authorizationController.GetUser(curUser.Id).ToString());
                        Console.ReadKey();
                        break;
                    case "8":
                        logout = true;
                        break;
                    default:
                        break;
                }
                Console.Clear();
            }
            Start();
        }
    }
}
