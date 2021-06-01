using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface ICourseManager
    {
        void ShowCourses(int userId);
        void ShowCreatedCourseByUser(int userId);
        void CreateCourse(int userId);
        void ShowSkills();
        void CreateSkill();
    }
}
