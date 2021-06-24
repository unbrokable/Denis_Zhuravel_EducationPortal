using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Application.Specification;
using AutoMapper.QueryableExtensions;
using Domain;
using Domain.Entities;
using Domain.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SkillService : IServiceSkill
    {
        private readonly IEntitiesRepository repository;
        private readonly IAutoMapperBLConfiguration mapper;

        public SkillService(IEntitiesRepository repository, IAutoMapperBLConfiguration mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task CreateAsync(SkillDTO data)
        {
            var skill = mapper.GetMapper().Map<SkillDTO, Skill>(data);
            await  repository.AddAsync<Skill>(skill);
        }

        public async Task<SkillDTO> GetByIdAsync(int id)
        {
            return mapper.GetMapper().Map<Skill, SkillDTO>(await repository.FindAsync<Skill>(SkillSpecification.FilterById(id)));
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await repository.FindAsync<Skill>(SkillSpecification.FilterById(id)) != null;
        }

        public async Task<bool> ExistNameAsync(string name)
        {
            return await repository.FindAsync<Skill>(SkillSpecification.FilterByName(name)) != null;
        }

        public async Task<IEnumerable<SkillDTO>> GetAllSkillsOfCourseAsync(int idCourse)
        {
           
            return (await repository.GetQueryAsync<Skill>(SkillSpecification.FilterByCourseId(idCourse)))
                .ProjectTo<SkillDTO>(mapper.GetMapper().ConfigurationProvider);
        }

        public async Task<IEnumerable<SkillUserDTO>> GetAllSkillsOfUserAsync(int idUser)
        {
            var user = await repository.FindAsync<User>( UserSpecification.FilterById(idUser), i => i.Include(i => i.Skills).ThenInclude(j => j.Skill));
            return user.Skills?.Join(user.Skills.Select(i => i.Skill), a => a.SkillId, b => b.Id, (a, b) => new SkillUserDTO
            {
                Id = b.Id,
                Name = b.Name,
                Level = a.Level
            });
        }
       
        public async Task<IEnumerable<SkillDTO>> GetAsync(int amount)
        {
            return ( await repository.GetQueryAsync<Skill>(new Specification<Skill>(i => true)))
                 .Take(amount)
                 .ProjectTo<SkillDTO>(mapper.GetMapper().ConfigurationProvider).ToList();
        }

    }
}
