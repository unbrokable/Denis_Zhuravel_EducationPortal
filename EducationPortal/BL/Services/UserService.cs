using System;
using System.Collections.Generic;
using Application.Interfaces;
using Application.DTO;
using AutoMapper;
using System.Linq;
using Domain;

namespace Application.Services
{
    public class UserService : IServiceUser
    {
        private readonly IEntitiesRepository bd;
        private readonly IHasher hasher;
        private readonly IAutoMapperBLConfiguration mapper;

        public UserService(IEntitiesRepository bd, IHasher hasher,IAutoMapperBLConfiguration mapper)
        {
            this.bd = bd;
            this.hasher = hasher;
            this.mapper = mapper;
        }

        public void Create(UserDTO data)
        {
            data.Password = hasher.Hash(data.Password);
            bd.Create(mapper.GetMapper().Map<UserDTO, User>(data));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return mapper.GetMapper().Map<IEnumerable<User>, List<UserDTO>>(bd.GetAll<User>());
        }

        public UserDTO GetById(int id)
        {
            var user = bd.GetBy<User>(i => i.Id == id);
            return mapper.GetMapper().Map<User, UserDTO>(user);
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
            var user = bd.GetBy<User>(i => String.Equals(password, i.Password) && String.Equals(email, i.Email));
            return mapper.GetMapper().Map<User, UserDTO>(user);
        }

        public UserDTO GetBy(Predicate<UserDTO> predicate)
        {
            return bd.GetBy(predicate);
        }

        public IEnumerable<UserDTO> GetAllBy(Predicate<UserDTO> predicate)
        {
            return bd.GetAllBy(predicate);
        }
    }
}
