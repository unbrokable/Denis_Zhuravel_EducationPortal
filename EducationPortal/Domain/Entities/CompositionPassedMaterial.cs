using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    [Serializable]
    public class CompositionPassedMaterial
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }

        public ICollection<int> MaterialsId { get; set; }
    }
}
