using Domain;
using Domain.Entities;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Specification
{
    class CourseSpecification
    {
        public static Specification<Course> FilterById(int id) => new Specification<Course>(i => i.Id == id);
        public static Specification<Course> FilterByNotUsed(int userId, IQueryable<int> passed) => new Specification<Course>(i => !passed.Contains(i.Id));
        public static Specification<Course> FilterByCreatorId(int idUser) => new Specification<Course>(i => i.UserId == idUser);
        public static Specification<Course> FilterByMaterial(int materialId) => new Specification<Course>(i => i.Materials.Select(j => j.Id).Contains(materialId));
    }
}
