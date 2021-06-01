using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.IServices
{
    public interface IServiceSkill:IServiceEntities<SkillDTO>
    {
        public bool Exist(int id);
        public bool ExistName(string name);
        IEnumerable<SkillDTO> GetAllSkillsOfCourse(int idCourse);
        IEnumerable<SkillUserDTO> GetAllSkillsOfUser(int idUser);
    }
}
