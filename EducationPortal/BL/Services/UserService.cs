using System;
using System.Collections.Generic;
using Application.Interfaces;
using Application.DTO;
using Domain;
using Application.Interfaces.IServices;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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


        public UserDTO GetById(int id) 
        {
            return GetUser(i => i.Id == id); 
        }

        public bool Create(string name, string email, string password, string password2)
        {
            try
            {
                if (ExistNameEmail(name,email))
                {
                    throw new Exception();
                }
                Create(new UserDTO { Name = name, Email = email, Password = password });
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
            return GetUser(i => i.Password == password && i.Email == email);
        }

        public UserDTO GetBy(Predicate<UserDTO> predicate)
        {
            return GetUser(predicate);
        }

        public IEnumerable<UserDTO> GetAllBy(Predicate<UserDTO> predicate)
        {
            return mapper.GetMapper().Map<IEnumerable<User>,IEnumerable<UserDTO>>(bd.GetAllBy<User>(i => predicate(mapper.GetMapper().Map<User, UserDTO>(i))));
        }

        public bool ExistNameEmail(string name, string email)
        {
            return bd.GetBy<User>(i => String.Compare(i.Email, email) == 0 || String.Compare(i.Name, name) == 0) != null; 
        }

        UserDTO GetUser(Predicate<UserDTO> predicate)
        {
            return mapper.GetMapper().Map < User,UserDTO>(bd.GetBy<User>(i => predicate(mapper.GetMapper().Map<User, UserDTO>(i)), i => i.Include(i => i.Skills).ThenInclude(i => i.Skill))); 
        }
    }
}
