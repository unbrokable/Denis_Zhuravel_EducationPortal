using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Interfaces
{
    interface IPassCourseManager
    {
        Task ChooseCourseAsync(int idUser);
        Task ChooseCourseToPassAsync(int idUser);
        Task PassCourseAsync(int idUser, int idCourse);
    }
}
