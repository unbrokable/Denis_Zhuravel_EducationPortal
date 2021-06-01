using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTO;
using EducationPortal.Interfaces;
using EducationPortal.Models;

namespace EducationPortal.Managers
{
    class SkillManager: ISkillManager
    {
        private readonly IServiceSkill serviceSkill;
        private readonly IAutoMapperUlConfiguration mapper;

        public SkillManager(IServiceSkill serviceSkill, IAutoMapperUlConfiguration mapper)
        {
            this.serviceSkill = serviceSkill;
            this.mapper = mapper;
        }

        public void Create()
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            if (serviceSkill.ExistName(name))
            {
                Console.WriteLine("This name exist");
                return;
            }
            serviceSkill.Create(new SkillDTO { Id = new Random().Next(1, 1000), Name = name });
            Console.WriteLine("Skill is saved");
        }

        public void Show()
        {
            var skills = mapper.GetMapper().Map<IEnumerable<SkillDTO>, IEnumerable<SkillViewModel>>(serviceSkill.GetAll());
            foreach (var skill in skills)
            {
                Console.WriteLine(skill.ToString());
            }
        }
    }
}
