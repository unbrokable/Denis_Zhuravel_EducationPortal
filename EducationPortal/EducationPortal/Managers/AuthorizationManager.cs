using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Application.Services;
using EducationPortal.Interfaces;
using EducationPortal.Models;
using EducationPortal.Models.Validators;
using System;

namespace EducationPortal
{
    class AuthorizationManager: IAuthorizationManager
    {
        private readonly IServiceUser service;

        public AuthorizationManager(IServiceUser service)
        {
            this.service = service;
        }

        public UserViewModel Enter()
        {
            Console.WriteLine("Hi user");
            string answer;
            while (true)
            {
                Console.WriteLine("Login 1\nCreate Account 2\nExit 3");
                answer = Console.ReadLine();
                switch (answer)
                {
                    case "1":
                        var user = Login();
                        if(user == null)
                        {
                            Console.WriteLine("Cant find account");
                        }
                        else
                        {
                            return user;
                        }                     
                        break;
                    case "2":
                        CreateAccount();
                        break;
                    case "3":
                        break;
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
            var validateResult = validator.Validate(new RegistrationModel() {
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

        public UserViewModel Login()
        {
            Console.WriteLine("_____________________Login_____________________");
            Console.WriteLine("Write Email");
            string email = Console.ReadLine();
            Console.WriteLine("Write Password");
            string password = Console.ReadLine();

            var curUser = service.Login(password, email);

            if ( curUser == null)
            {
                return null;
            }
            var mapper = new MapperConfiguration(i => {
                i.CreateMap<UserDTO, UserViewModel>();
                i.CreateMap<UserViewModel,UserDTO>();
            });
            return mapper.CreateMapper().Map<UserDTO, UserViewModel>(curUser);
        }
    }
}
