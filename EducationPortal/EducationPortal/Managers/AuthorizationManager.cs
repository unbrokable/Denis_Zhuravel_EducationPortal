using AutoMapper;
using Application.DTO;
using Application.Interfaces;
using Application.Services;
using EducationPortal.Interfaces;
using EducationPortal.Models;
using EducationPortal.Models.Validators;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EducationPortal
{
    class AuthorizationManager: IAuthorizationManager
    {
        private readonly IServiceUser service;
        private readonly ILogger logger;

        public AuthorizationManager(IServiceUser service, ILogger logger)
        {
            this.service = service;
            this.logger = logger;
        }

        public async Task<UserViewModel> EnterAsync()
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
                        var user = await LoginAsync();
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
                        await CreateAccountAsync();
                        break;
                    case "3":
                        break;
                    default:
                        break;
                }
            }
            
        }

        public async Task<bool> CreateAccountAsync()
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
                logger.LogInformation("Invalid data of registration" + validateResult.ToString(","));
                Console.WriteLine(validateResult.ToString(","));
                return false;
            }

            if(await service.CreateAsync(name, email, password, password2))
            {
                Console.WriteLine("Created");
                return true;
            }
            Console.WriteLine("Email or Login Invalid");
            return false;
            
        }

        public async Task<UserViewModel> LoginAsync()
        {
            Console.WriteLine("_____________________Login_____________________");
            Console.WriteLine("Write Email");
            string email = Console.ReadLine();
            Console.WriteLine("Write Password");
            string password = Console.ReadLine();

            var curUser = await service.LoginAsync(password, email);

            if ( curUser == null)
            {
                return null;
            }
            var mapper = new MapperConfiguration(i => {
                i.CreateMap<SkillUserDTO, SkillUserViewModel>();
                i.CreateMap<UserDTO, UserViewModel>();
            });
            return mapper.CreateMapper().Map<UserDTO, UserViewModel>(curUser);
        }

        public async Task<UserViewModel> GetUserAsync(int id)
        {
            var curUser = await service.GetByIdAsync(id);

            if (curUser == null)
            {
                return null;
            }
            var mapper = new MapperConfiguration(i => {
                i.CreateMap<SkillUserDTO, SkillUserViewModel>();
                i.CreateMap<UserDTO, UserViewModel>();
            });
            return mapper.CreateMapper().Map<UserDTO, UserViewModel>(curUser);

        }
    }
}
