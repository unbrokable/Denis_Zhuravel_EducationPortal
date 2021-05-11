using System;
using System.Collections.Generic;
using System.Text;
using BL.Interfaces;
using BL.DTO;
using BL.Validators;
using DL;
using DL.Repositories;
using AutoMapper;
using System.Text.RegularExpressions;
using System.Linq;

namespace BL.Services
{
    public class UserService : IService<UserDTO>
    {
        UserRepository bd;
        IHasher hasher;
        Validator validator = new Validator();
        public UserService(UserRepository bd, IHasher hasher)
        {
            this.bd = bd;
            this.hasher = hasher ;
        }
        public void Create(UserDTO data)
        {
            data.Password = hasher.Hash(data.Password);
            var user = new MapperConfiguration(i => i.CreateMap<UserDTO, User>()).CreateMapper().Map<UserDTO, User>(data);
            bd.Create(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var mapper = new MapperConfiguration(i => i.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(bd.GetAll());
        }

        public UserDTO GetById(int id)
        {
            var user = bd.GetById(id);
            return new MapperConfiguration(i => i.CreateMap<User, UserDTO>()).CreateMapper().Map<User,UserDTO >(user);
        }

        public string Create(string name, string email, string password, string password2)
        {
            String errors = validator.ValidateAccount(name,email,password,password2);
            if (errors.Length == 0)
            {
                Create(new UserDTO {Id = new Random().Next(0,100), Name = name, Email = email, Password = password});
                return null;
            }
            return errors.ToString();
        }
        public UserDTO Login(string password, string email)
        {
            password = hasher.Hash(password);
            return GetAll().ToList().FirstOrDefault(i=> String.Equals(password, i.Password)  && String.Equals(email, i.Email));
        }
    }
}
