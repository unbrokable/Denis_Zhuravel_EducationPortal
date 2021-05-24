using BL.DTO;
using BL.Interfaces;
using BL.Services;
using EducationPortal.Interfaces;
using EducationPortal.Models;
using EducationPortal.Models.Validators;
using System;

namespace EducationPortal.Controllers
{
    class AuthorizationManager: IAuthorizationManager
    {
        IServiceUser service;
        public AuthorizationManager(IServiceUser service)
        {
            this.service = service;
        }
        public void Enter()
        {
            Console.WriteLine("Hi user");
            string answer;
            while (true)
            {
                Console.WriteLine("Login 1 | Create Account 2 | Exit 3");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        UserDTO user = Login();
                        if (user == null)
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

            var validator = new RegistrationValidator();
            var validateResult = validator.Validate(new RegistrationModel()
            {
                Name = name,
                Password = password,
                ConfirmPassword = password2,
                Email = email

            });
            if (!validateResult.IsValid)
            {
                Console.WriteLine(validateResult.ToString(","));
                return false;
            }
            service.Create(name, email, password, password2);
            Console.WriteLine("Created");
            return true;
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
