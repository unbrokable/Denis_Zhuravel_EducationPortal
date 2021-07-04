
using System;

namespace Domain.Entities
{
    public class CompositionSkillUser:Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int SkillId { get; set; }
        public virtual Skill Skill { get; set; }

        public int Level { get; set; }

    }
}
