using Domain.Entities;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specification
{
    class PassedMaterialSpecification
    {
        public static Specification<CompositionPassedMaterial> FilterByUserId(int id) => new Specification<CompositionPassedMaterial>(i => i.UserId == id);
        public static Specification<CompositionPassedMaterial> FilterByMaterialId(int id) => new Specification<CompositionPassedMaterial>(i => i.MaterialId == id);
    }
    class PassedCourseSpecification
    {
        public static Specification<CompositionPassedCourse> FilterByUserId(int id) => new Specification<CompositionPassedCourse>(i => i.UserId == id);
        public static Specification<CompositionPassedCourse> FilterByCourseId(int id) => new Specification<CompositionPassedCourse>(i => i.CourseId == id);
    }
}
