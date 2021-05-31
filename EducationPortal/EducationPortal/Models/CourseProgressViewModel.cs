using System;
using System.Collections.Generic;
using System.Text;

namespace EducationPortal.Models
{
    class CourseProgressViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MaterialViewModel> MaterialsPassed { get; set; }
        public ICollection<MaterialViewModel> MaterialsNotPassed { get; set; }

        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();
            @string.Append($"+++++++++++Course {Id} {Name} ++++++++++++\n")
                .Append(string.Concat(Description,'\n',"Passed material\n"));
            foreach (var item in MaterialsPassed)
            {
                @string.Append(String.Concat(item.ToString(), '\n'));
            }
            @string.AppendLine("Dont passed material");
            foreach (var item in MaterialsNotPassed)
            {
                @string.Append(item.Id).Append(" ");
            }
            return @string.ToString();
        }
    }
}
