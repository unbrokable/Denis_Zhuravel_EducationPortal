using System;
using System.Collections.Generic;
using Application.Interfaces;
using Application.DTO;
using Domain;
using Application.Interfaces.IServices;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Specification;
using Domain.Specifications;
using Application.Specification;
using AutoMapper.QueryableExtensions;
using System.Threading.Tasks;

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

        public async Task CreateAsync(UserDTO data)
        {
            data.Password = hasher.Hash(data.Password);
            await bd.AddAsync(mapper.GetMapper().Map<UserDTO, User>(data));
        }


        public async Task<UserDTO> GetByIdAsync(int id) 
        {
            return mapper.GetMapper().Map<User, UserDTO>( await bd.FindAsync<User>(UserSpecification.FilterById(id))); 
        }

        public async Task<bool> CreateAsync(string name, string email, string password, string password2)
        {
            try
            {
                if (await ExistNameEmailAsync(name,email))
                {
                    throw new Exception();
                }
                await CreateAsync(new UserDTO { Name = name, Email = email, Password = password });
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<UserDTO> LoginAsync(string password, string email)
        {
            password = hasher.Hash(password);
            return mapper.GetMapper().Map < User,UserDTO>(await bd.FindAsync<User>(
                UserSpecification.Login(email, password), 
                i => i.Include(i => i.Skills).ThenInclude(i => i.Skill))); 
        }

        public async Task<bool> ExistNameEmailAsync(string name, string email)
        {
            return await bd.FindAsync<User>(UserSpecification.FilterByName(name).Or(UserSpecification.FilterByEmail(email))) != null; 
        }

        public async Task<IEnumerable<UserDTO>> GetAsync(int amount)
        {
            return (await bd.GetQueryAsync<User>(new Specification<User>(i => true))).Take(amount)
                .ProjectTo<UserDTO>(mapper.GetMapper().ConfigurationProvider).ToList();
        }
    }
}
