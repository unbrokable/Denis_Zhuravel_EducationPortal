using System.Collections.Generic;
using Application.Interfaces;
using System.Linq;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;
using Domain.Specification;

namespace Infrastructure.Repositories
{
   public class EntitiesRepository : IEntitiesRepository
    {
        private readonly ApplicationContext bd;

        public EntitiesRepository(ApplicationContext bd)
        {
            this.bd = bd;
        }

        public async Task<bool> AddAsync<T>(T data) where T : class
        {
            try
            {
                await bd.Set<T>().AddAsync(data);
                await bd.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Task<T> FindAsync<T>(Specification<T> specification, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {
            return Task.FromResult(Include<T>(include).FirstOrDefault(specification.Expression));
        }



        public Task<IEnumerable<T>> GetAsync<T>(Specification<T> specification, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {
            return Task.FromResult(Include<T>(include).Where(specification.Expression).AsEnumerable());
        }

        public  Task<IQueryable<T>> GetQueryAsync<T>(Specification<T> specification, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class
        {
            return Task.FromResult(Include<T>(include).Where(specification.Expression));
        }

        public async Task<bool> RemoveAsync<T>(T data) where T : class
        {
            try
            {
               await Task.Run(() => bd.Remove(data));
                await bd.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveAsync<T>(int id) where T : class
        {
            try
            {
                bd.Remove(await bd.Set<T>().FindAsync(id));
                await bd.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        public async Task<bool> UpdateAsync<T>(T data) where T : class
        {
            try
            {
                bd.Set<T>().Update(data);
                await bd.SaveChangesAsync();
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
