using Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
    public interface IServiceSkill:IServiceEntities<SkillDTO>
    {
        Task<bool> ExistAsync(int id);
        Task<bool> ExistNameAsync(string name);
        Task<IEnumerable<SkillDTO>> GetAllSkillsOfCourseAsync(int idCourse);
        Task<IEnumerable<SkillUserDTO>> GetAllSkillsOfUserAsync(int idUser);

    }
}
