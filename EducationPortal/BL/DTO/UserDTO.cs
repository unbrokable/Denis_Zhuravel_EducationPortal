using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<CourseDTO> CreatedCourses { get; set; }
        public ICollection<int> CreatedCoursesId { get; set; }
    }
}
