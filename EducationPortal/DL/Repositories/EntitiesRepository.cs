
using System.Collections.Generic;
using Application.Interfaces;
using System.Linq;
using System;

namespace Infrastructure.Repositories
{
   public class EntitiesRepository : IEntitiesRepository
    {
        private readonly IHandler bd;

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

        public T GetBy<T>(Predicate<T> predicate) where T : class
        {
            return GetAllBy<T>(predicate)?.FirstOrDefault();
        }

        public IEnumerable<T> GetAllBy<T>(Predicate<T> predicate) where T : class
        {
            var arr = GetAll<T>();
            return arr.Where(i => predicate(i))??null;
        }

        public bool Update<T>(T data, Predicate<T> predicate) where T : class
        {
            List<T> entities =  bd.Load<T>().ToList();
            var entity = entities.FirstOrDefault(i => predicate(i));
            if(entity  == null)
            {
                return false;
            }
            entities[entities.IndexOf(entity)] = data; 
            return bd.Save<T>(entities);
        }
    }
}
