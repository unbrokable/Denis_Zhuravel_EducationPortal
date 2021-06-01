using Application.DTO.MaterialDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CourseProgressDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MaterialDTO> MaterialsPassed { get; set; }
        public ICollection<MaterialDTO> MaterialsNotPassed { get; set; }

        public ICollection<SkillDTO> Skills { get; set; }
    }
}
