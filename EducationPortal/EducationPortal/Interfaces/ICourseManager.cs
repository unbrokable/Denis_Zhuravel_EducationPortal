using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Interfaces
{
    interface ICourseManager
    {
        void ShowCreatedCourseByUser(int userId);
        void CreateCourse(int userId);
    }
}
