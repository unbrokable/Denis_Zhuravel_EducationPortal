using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models
{
    abstract class MaterialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int CreatorId { get; set; }

        public override string ToString()
        {
            return $" ID {Id}\n Name {Name}\n Location {Location}";
        }
    }
}
