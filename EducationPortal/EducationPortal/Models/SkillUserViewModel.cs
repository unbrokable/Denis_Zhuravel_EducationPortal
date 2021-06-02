using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models
{
    class SkillUserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        
        public override string ToString()
        {
            return $"Skill: Id {Id} Name {Name} Level {Level}";
        }
    }
}
