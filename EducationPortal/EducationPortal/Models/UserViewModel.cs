using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EducationPortal.Models
{
    class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<SkillUserViewModel> Skills { get; set; }

        public override string ToString()
        {
            return $"Name {Name} Emaei {Email}\nSkills\n{String.Join('\n', Skills.Select(i => i.ToString()))}";
        }
    }
}
