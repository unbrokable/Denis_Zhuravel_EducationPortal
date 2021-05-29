using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [Serializable]
    public class CompositionCourseMaterial
    {
        public int MaterialId { get; set; }
        public int CourseId { get; set; }
    }
}
