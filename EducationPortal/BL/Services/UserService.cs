using System;
using System.Collections.Generic;
using System.Text;
using BL.Interfaces;
using BL.DTO;
using DL;
using DL.Repositories;
using AutoMapper;
using System.Text.RegularExpressions;
using System.Linq;
using DL.Interfaces;

namespace BL.Services
{
    public class UserService : IServiceUser
    {
        IEntitiesRepository bd;
        IHasher hasher;
        IAutoMapperBLConfiguration mapper;

        public UserService(IEntitiesRepository bd, IHasher hasher, IAutoMapperBLConfiguration mapper)
        {
            this.bd = bd;
            this.hasher = hasher;
            this.mapper = mapper;
        }
        public void Create(UserDTO data)
        {
            data.Password = hasher.Hash(data.Password);
            var user = mapper.CreateMapper().Map<UserDTO, User>(data);
            bd.Create(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
           
            return mapper.CreateMapper().Map<IEnumerable<User>, List<UserDTO>>(bd.GetAll<User>());
        }

        public UserDTO GetById(int id)
        {
            var user = bd.GetAll<User>().FirstOrDefault(i => i.Id == id);
            return mapper.CreateMapper().Map<User,UserDTO >(user);
        }

        public bool Create(string name, string email, string password, string password2)
        {
            try
            {
                Create(new UserDTO { Id = new Random().Next(1, 1000), Name = name, Email = email, Password = password });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        public UserDTO Login(string password, string email)
        {
            password = hasher.Hash(password);
            return GetAll().ToList().FirstOrDefault(i=> String.Equals(password, i.Password)  && String.Equals(email, i.Email));
        }
    }
}
