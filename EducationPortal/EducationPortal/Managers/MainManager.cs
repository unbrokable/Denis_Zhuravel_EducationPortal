using EducationPortal.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async Task StartAsync()
        {
            var curUser = await authorizationController.EnterAsync();
            Console.Clear();
            Console.WriteLine($"Hi {curUser.Id} {curUser.Name} \nYour email {curUser.Email} \n ");
            string answer;
            bool logout = false;
            while (!logout)
            {
                Console.WriteLine(String.Concat("Create Material\t1\nCreate Skills\t2\nCreate course\t3\n",
                    "Courses by you\t4\nPass course\t5\nChoose course\t6\nDelete course\t7\nDelete material\t8\n",
                    "About me\t9\nLog out\t10\n"));
                answer = Console.ReadLine();
                Console.Clear();
                switch (answer)
                {
                    case "1":
                        try
                        {
                           await materialManager.CreateMaterialAsync(curUser.Id);
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid data for material");
                        }
                        break; 
                    case "2":
                        await courseController.CreateSkillAsync();
                        break;
                    case "3":
                        Console.WriteLine("Materials\n");
                        foreach (var item in  materialManager.ShowAvaibleMaterialAsync(curUser.Id).Result)
                        {
                            Console.Write(item.ToString());
                        }
                        Console.WriteLine("Skills\n");
                        await courseController.ShowSkillsAsync();
                        try
                        {
                            await courseController.CreateCourseAsync(curUser.Id);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid data for course");
                        }
                        break;
                    case "4":
                        await courseController.ShowCreatedCourseByUserAsync(curUser.Id);
                        break;
                    case "5":
                        await passCourseManager.ChooseCourseToPassAsync(curUser.Id);
                        break;
                    case "6":
                        await passCourseManager.ChooseCourseAsync(curUser.Id);
                        break;
                    case "7":
                        await courseController.Remove(curUser.Id);
                        break;
                    case "8":
                        await materialManager.Remove(curUser.Id);
                        break;
                    case "9":
                        Console.WriteLine( authorizationController.GetUserAsync(curUser.Id).Result.ToString());
                        break;
                    case "10":
                        logout = true;
                        break;
                    default:
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
           await StartAsync();
        }
    }
}
