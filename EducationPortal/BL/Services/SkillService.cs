using Application.DTO;
using Application.Interfaces;
using Application.Interfaces.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Create(SkillDTO data)
        {
            var skill = mapper.GetMapper().Map<SkillDTO, Skill>(data);
            if (skill != null)
                repository.Create<Skill>(skill);
        }

        public IEnumerable<SkillDTO> GetAllBy(Predicate<SkillDTO> predicate)
        {
            return mapper.GetMapper()
                .Map<IEnumerable<Skill>, IEnumerable<SkillDTO>>(repository.GetAllBy<Skill>(i => predicate(mapper.GetMapper().Map<Skill, SkillDTO>(i))));
        }

        public SkillDTO GetBy(Predicate<SkillDTO> predicate)
        {
            return mapper.GetMapper()
                .Map<Skill, SkillDTO>(repository.GetBy<Skill>(i => predicate(mapper.GetMapper().Map<Skill,SkillDTO>(i))));
        }

        public SkillDTO GetById(int id)
        {
            return mapper.GetMapper().Map<Skill, SkillDTO>(repository.GetBy<Skill>(i => i.Id == id));
        }

        public bool Exist(int id)
        {
            return repository.GetBy<Skill>(i => i.Id == id) != null;
        }

        public bool ExistName(string name)
        {
            return repository.GetBy<Skill>(i => String.Compare(i.Name, name) == 0) != null;
        }

        public IEnumerable<SkillDTO> GetAllSkillsOfCourse(int idCourse)
        {
           
            return mapper.GetMapper()
                .Map<IEnumerable< Skill>, IEnumerable<SkillDTO>>( repository.GetAllBy<Skill>(i => i.Courses.Select(j => j.Id).Contains(idCourse)));
        }

        public IEnumerable<SkillUserDTO> GetAllSkillsOfUser(int idUser)
        {
            var skillsId = repository.GetAllBy<CompositionSkillUser>(i => i.UserId == idUser);
  
            var skills = GetAllBy(i => skillsId?.Select(i => i.SkillId).Contains(i.Id) ?? false);
            return skillsId?.Join(skills, a => a.SkillId, b => b.Id, (a, b) => new SkillUserDTO
            {
                Id = b.Id,
                Name = b.Name,
                Level = a.Level
            });
        }
    }
}
