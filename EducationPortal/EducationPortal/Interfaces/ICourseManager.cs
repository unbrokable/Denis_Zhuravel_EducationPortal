using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface ICourseManager
    {
        void ShowCourses();
        void ShowCreatedCourseByUser(int userId);
        void CreateCourse(int userId);
    }
}
