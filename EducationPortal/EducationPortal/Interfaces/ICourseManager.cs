using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Interfaces
{
    interface ICourseManager
    {
        Task ShowCoursesAsync(int userId);
        Task ShowCreatedCourseByUserAsync(int userId);
        Task CreateCourseAsync(int userId);
        Task ShowSkillsAsync();
        Task CreateSkillAsync();
        Task Remove(int idUser);
    }
}
