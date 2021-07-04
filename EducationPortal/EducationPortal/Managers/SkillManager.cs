using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using Application.DTO;
using EducationPortal.Interfaces;
using EducationPortal.Models;
using System.Threading.Tasks;

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

        public async Task CreateAsync()
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            if (await serviceSkill.ExistNameAsync(name))
            {
                Console.WriteLine("This name exist");
                return;
            }
            await serviceSkill.CreateAsync(new SkillDTO {  Name = name });
            Console.WriteLine("Skill is saved");
        }

        public async Task ShowAsync()
        {
            //page change 
            var skills = mapper.GetMapper().Map<IEnumerable<SkillDTO>, IEnumerable<SkillViewModel>>( await serviceSkill.GetAsync(100));
            foreach (var skill in skills)
            {
                Console.WriteLine(skill.ToString());
            }
        }
    }
}
