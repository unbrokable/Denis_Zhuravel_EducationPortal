using Domain;
using Domain.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specification
{
    class MaterialSpecification
    {
        public static Specification<Material> FilterById(int id) => new Specification<Material>(i => i.Id == id);
        public static Specification<Material> FilterByCreator(int idUser) => new Specification<Material>(i => i.CreatorId == idUser);
    }
}
