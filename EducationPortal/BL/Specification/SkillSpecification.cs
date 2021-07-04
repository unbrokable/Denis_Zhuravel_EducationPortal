using Domain.Entities;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Specification
{
    class SkillSpecification
    {
        public static Specification<Skill> FilterById(int id) => new Specification<Skill>(i => i.Id == id);
        public static Specification<Skill> FilterByCourseId(int courseId) => new Specification<Skill>(i => i.Courses.Select(j => j.Id).Contains(courseId));
        public static Specification<Skill> FilterByName(string name) => new Specification<Skill>(i => i.Name == name);
    }
}
