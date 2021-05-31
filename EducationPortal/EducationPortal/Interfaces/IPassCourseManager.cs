using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface IPassCourseManager
    {
        public void ChooseCourse(int idUser);
        public void ChooseCourseToPass(int idUser);
        public void PassCourse(int idUser, int idCourse);
    }
}
