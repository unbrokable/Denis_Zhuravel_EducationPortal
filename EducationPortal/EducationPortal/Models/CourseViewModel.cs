using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models
{
    class CourseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MaterialViewModel> Materials { get; set; }


        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();

            @string.Append($"______________Course {Id} {Name}_______________\n")
                .Append($"Description: {Description}\n")
                .Append($"--------Materials---------\n");
            
            foreach (var item in Materials)
            {
                @string.Append(item.ToString()).Append("\n");
            }

            return @string.ToString();
        }
    }
}
