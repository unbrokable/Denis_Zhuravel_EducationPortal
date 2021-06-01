using System;

namespace Domain
{
    [Serializable]
    public class CompositionCourseMaterial
    {
        public int MaterialId { get; set; }
        public int CourseId { get; set; }
    }
}
