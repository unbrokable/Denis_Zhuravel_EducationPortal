using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    [Serializable]
    public abstract class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public int? CreatorId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<CompositionPassedMaterial> Users { get; set; }
        //public virtual ICollection<CompositionCourseMaterial> Courses {get; set;}
        public virtual ICollection<Course> Courses { get; set; }
    }
}
