using System;

namespace Domain.Entities
{
    [Serializable]
    public class CompositionSkillUser
    {
        public int UserId { get; set; }
        public int SkillId { get; set; }

        public int Level { get; set; }
    }
}
