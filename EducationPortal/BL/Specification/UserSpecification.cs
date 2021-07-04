using Domain;
using Domain.Specification;
using Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Specification
{
    public class UserSpecification
    {
        public static Specification<User> Login(string email, string password)
        {
            return new Specification<User>(i => i.Email == email)
                .And(new Specification<User>(i => i.Password == password));
        }
        public static Specification<User> FilterById(int id) => new Specification<User>(i => i.Id == id);
        public static Specification<User> FilterByName(string name) => new Specification<User>(i => i.Name == name);
        public static Specification<User> FilterByEmail(string email) => new Specification<User>(i => i.Email == email);
    }
}
