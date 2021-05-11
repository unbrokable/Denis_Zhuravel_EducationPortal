using System;
using System.Collections.Generic;
using System.Text;
using DL.Interfaces;
using System.Linq;
namespace DL.Repositories
{
   public class UserRepository : IRepository<User>
    {
        IContext<IEnumerable< User>> bd;
        public UserRepository(IContext<IEnumerable<User>> bd)
        {
            this.bd = bd;
        }
        public bool Create(User data)
        {
            List<User> users = (GetAll()??new List<User>()).ToList();
            users.Add(data);
            bool result = bd.Save(users);
            return result;
        }
        public IEnumerable<User> GetAll()
        {
            return bd.Load()?.ToList(); 
        }
        public User GetById(int id)
        {
            IEnumerable<User> users = GetAll();
            return users.FirstOrDefault(i => i.Id == id);
        }
    }
}
