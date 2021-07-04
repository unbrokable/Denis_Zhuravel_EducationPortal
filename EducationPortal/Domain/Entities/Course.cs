using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Course:Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public int? UserId { get; set;}
        public virtual User User { get; set; }

         public ICollection<Material> Materials { get; set; }
         public ICollection<Skill> Skills { get; set; }

        public virtual ICollection<CompositionPassedCourse> Users { get; set; }

    }
}
