
using System.Collections.Generic;
using Application.Interfaces;
using System.Linq;
namespace Infrastructure.Repositories
{
   public class EntitiesRepository : IEntitiesRepository
    {
        IHandler bd;
        public EntitiesRepository(IHandler bd)
        {
            this.bd = bd;
        }
        public bool Create<T>(T data) where T : class
        {
            List<T> fromBd = GetAll<T>()?.ToList() ?? new List<T>();

            fromBd.Add(data);
            bool result = bd.Save(fromBd);
            return result;
        }

        public IEnumerable<T> GetAll<T>() where T : class
        {
            return bd.Load<T>();
        }

        public bool Update<T>(IEnumerable<T> data) where T : class
        {
            return bd.Save<T>(data);
        }
    }
}
