using BL.DTO;
using BL.Services;
using System;

namespace EducationPortal.Controllers
{
    class AuthorizationController
    {
        UserService service; 
        public AuthorizationController(UserService service)
        {
            this.service = service;
        }
        public void Index()
        {
            Console.WriteLine("Hi user");
            string answer;
            while (true) { 
                Console.WriteLine("Login 1 | Create Account 2 | Exit 3");
                answer = Console.ReadLine();
            switch (answer)
            {
                case "1":
                    UserDTO user = Login();
                    if(user == null)
                    {
                        Console.WriteLine("Cant find user with this name");
                    }
                    else
                    {
                        Console.WriteLine($"Hi {user.Id} {user.Name} \n Your email {user.Email} \n ");
                    }
                    break;
                case "2":
                        CreateAccount();
                    break;
                case "3":
                    return;
                default:
                    break;
            }
            }
        }
        public bool CreateAccount()
        {
            Console.WriteLine("_____________________Create Account_____________________");
            Console.WriteLine("Write Name");
            string name = Console.ReadLine();
            Console.WriteLine("Write Email");
            string email = Console.ReadLine();
            Console.WriteLine("Write Password");
            string password = Console.ReadLine();
            Console.WriteLine("Repeat Password");
            string password2 = Console.ReadLine();
            if (service.Create(name, email, password, password2) == null)
            {
                Console.WriteLine("Created");
                return true;
            }
            Console.WriteLine(service.Create(name, email, password, password2));
            return false;
        }
        public UserDTO Login()
        {
            Console.WriteLine("_____________________Login_____________________");
            Console.WriteLine("Write Email");
            string email = Console.ReadLine();
            Console.WriteLine("Write Password");
            string password = Console.ReadLine();
            return service.Login(password, email);
        }
    }
}
