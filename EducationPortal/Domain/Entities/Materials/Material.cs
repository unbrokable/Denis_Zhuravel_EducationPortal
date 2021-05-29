using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    [Serializable]
    public abstract class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public int CreatorId { get; set; }
    }
}
