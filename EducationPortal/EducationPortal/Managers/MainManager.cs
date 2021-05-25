using EducationPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Controllers
{
    class MainManager: IManager
    {
        readonly ICourseManager courseController;
        readonly IAuthorizationManager authorizationController;
        readonly IMaterialManager materialManager;
        public MainManager(ICourseManager courseController, IAuthorizationManager authorizationController, IMaterialManager materialController )
        {
            this.materialManager = materialController;
            this.authorizationController = authorizationController;
            this.courseController = courseController;
        }

        public void Start()
        {
            var curUser = authorizationController.Enter();
      
            Console.WriteLine($"Hi {curUser.Id} {curUser.Name} \n Your email {curUser.Email} \n ");
            string answer;
            bool logout = false;
            while (!logout)
            {
                Console.WriteLine("Create Material 1\nCreate Course 2\nSee all your course 3\nLog out 4");
                answer = Console.ReadLine();
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
                        
                        break;
                    case "2":
                        Console.WriteLine("Materials");
                        foreach (var item in materialManager.ShowAvaibleMaterial(curUser.Id))
                        {
                            Console.WriteLine(item.ToString());
                        }
                        try
                        {
                            courseController.CreateCourse(curUser.Id);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid data for course");
                        }
                        break;
                    case "3":
                        courseController.ShowCreatedCourseByUser(curUser.Id);
                        break;
                    case "4":
                        logout = true;
                        break;
                    default:
                        break;
                }

            }
            Start();
        }
    }
}
