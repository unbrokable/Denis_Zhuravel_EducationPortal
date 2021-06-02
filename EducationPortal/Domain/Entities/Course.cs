using System;
using System.Collections.Generic;

namespace Domain
{
    [Serializable]
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set;}

        public ICollection<int> MaterialsId { get; set; }
        public ICollection<int> SkillsId { get; set; }

    }
}
