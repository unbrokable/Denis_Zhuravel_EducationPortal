using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CompositionPassedCourse:Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public bool IsPassed { get; set; }
    }
}
