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
        public static Specification<Course> FilterByCreatorId(int idUser) => new Specification<Course>(i => i.UserId == idUser);
        public static Specification<Course> FilterByMaterial(int materialId) => new Specification<Course>(i => i.Materials.Select(j => j.Id).Contains(materialId));
        public static Specification<Course> FilterByNotChoosenByUser(int userId) => new Specification<Course>(i => !i.Users.Select(u => u.UserId).Contains(userId));
    }
}
