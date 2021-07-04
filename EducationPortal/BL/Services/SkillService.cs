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

            return (await repository.GetCustomSelectAsync<Skill, SkillDTO>(SkillSpecification.FilterByCourseId(idCourse), mapper.GetMapper().ConfigurationProvider));
        }

        public async Task<IEnumerable<SkillUserDTO>> GetAllSkillsOfUserAsync(int idUser)
        {
            var user = await repository.FindAsync<User>( UserSpecification.FilterById(idUser), i => i.Skills.Select(s => s.Skill));
            return user.Skills?.Join(user.Skills.Select(i => i.Skill), a => a.SkillId, b => b.Id, (a, b) => new SkillUserDTO
            {
                Id = b.Id,
                Name = b.Name,
                Level = a.Level
            });
        }
       
        // change on page after mvc 
        public async Task<IEnumerable<SkillDTO>> GetAsync(int amount)
        {
            var page = await repository.GetAsync<Skill>(0, amount);
            return mapper.GetMapper().Map<IEnumerable<SkillDTO>>(page.Items).ToList();
        }

    }
}
