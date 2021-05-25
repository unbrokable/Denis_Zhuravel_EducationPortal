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
        readonly IEntitiesRepository bd;
        readonly IHasher hasher;
        readonly IAutoMapperBLConfiguration mapper;

        public UserService(IEntitiesRepository bd, IHasher hasher,IAutoMapperBLConfiguration mapper)
        {
            this.bd = bd;
            this.hasher = hasher;
            this.mapper = mapper;
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
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(bd.GetAll<User>());
        }

        public UserDTO GetById(int id)
        {
            var user = bd.GetAll<User>().FirstOrDefault(i => i.Id == id);
            return mapper.CreateMapper().Map<User, UserDTO>(user);
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
            return GetAll().ToList().FirstOrDefault(i => String.Equals(password, i.Password) && String.Equals(email, i.Email));
        }

        public UserDTO GetBy(Predicate<UserDTO> predicate)
        {
            return GetAll().FirstOrDefault(i => predicate(i));
        }

        public IEnumerable<UserDTO> GetByAll(Predicate<UserDTO> predicate)
        {
            return GetAll().Where(i => predicate(i));
        }
    }
}
