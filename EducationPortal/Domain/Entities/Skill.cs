using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Skill:Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompositionSkillUser> Users { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
