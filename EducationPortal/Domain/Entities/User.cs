using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain
{
   [Serializable]
   public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Course> CreatedCourses { get; set; }
        public ICollection<CompositionPassedMaterial> PassedMaterials { get; set; }
        public ICollection<CompositionSkillUser> Skills { get; set; }
        public ICollection<Material> Materials { get; set; }
        public ICollection<CompositionPassedCourse> PassedCourses { get; set; }
        //public ICollection<int> CreatedCoursesId { get; set; }
    }
}
