
using System.Collections.Generic;
using Application.Interfaces;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Repositories
{
   public class EntitiesRepository : IEntitiesRepository
    {
        private readonly ApplicationContext bd;

        public EntitiesRepository(ApplicationContext bd)
        {
            this.bd = bd;
        }

        public bool Create<T>(T data) where T : class
        {
            try
            {
                bd.Set<T>().Add(data);
                bd.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public T GetBy<T>(Func<T,bool> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {

            Expression<Func<T, bool>> expression = i => predicate(i);

            return Include<T>(include).FirstOrDefault(expression.Compile());
        }

        public IEnumerable<T> GetAllBy<T>(Func<T, bool> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {
            Expression<Func<T, bool>> expression = i => predicate(i);
            return Include<T>(include).Where(expression.Compile());
        }

        public bool Update<T>(T data) where T : class
        {
            try
            {
                bd.Set<T>().Update(data);
                bd.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        IQueryable<T> Include<T>(Func<IQueryable<T>, IIncludableQueryable<T, object>> include ) where T : class
        {
            IQueryable<T> query = bd.Set<T>().AsQueryable();
            if (include != null)
            {
                query = include(query);
            }
            return query;
        }
    }
}
