using Application.DTO.MaterialDTOs;
using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }

        public ICollection<MaterialDTO> Materials { get; set; }
        public ICollection<int> MaterialsId { get; set; }

        public ICollection<SkillDTO> Skills { get; set; }
        public ICollection<int> SkillsId { get; set; }

    }
}
