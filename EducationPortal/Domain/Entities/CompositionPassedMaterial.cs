using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    [Serializable]
    public class CompositionPassedMaterial
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }
    }
}
