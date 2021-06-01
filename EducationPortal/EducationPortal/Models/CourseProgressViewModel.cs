using System;
using System.Collections.Generic;
using System.Linq;
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
        public ICollection<SkillViewModel> Skills { get; set; }

        public override string ToString()
        {
            StringBuilder @string = new StringBuilder();
            @string.Append($"+++++++++++Course {Id} {Name} ++++++++++++\n")
                .Append(string.Concat(Description,'\n',"Passed material\n"));
            foreach (var item in MaterialsPassed)
            {
                @string.Append(String.Concat(item.ToString(), '\n'));
            }
            if (MaterialsNotPassed.Any())
            {
                @string.AppendLine("Dont passed material");
                foreach (var item in MaterialsNotPassed)
                {
                    @string.Append(item.Id).Append(" ");
                }

            }
            else
            {
                @string.AppendLine("You passed this course and get some skills");
                foreach (var skill in Skills)
                {
                    @string.AppendLine(skill.ToString());
                }
            }
            
            return @string.ToString();
        }
    }
}
