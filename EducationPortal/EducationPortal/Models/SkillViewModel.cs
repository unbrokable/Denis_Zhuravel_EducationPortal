﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models
{
    class SkillViewModel
    {
        public int Id { get; set; }
        public string Name{ get; set; }

        public override string ToString()
        {
            return $"Skill: Id {Id} Name {Name}";
        }
    }
}
