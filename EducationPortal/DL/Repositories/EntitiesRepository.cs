using System.Collections.Generic;
using Application.Interfaces;
using System.Linq;
using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using System.Threading.Tasks;
using Domain.Specification;
using Microsoft.EntityFrameworkCore;
using Domain;
using Infrastructure.Extantion;
using Domain.Entities;

namespace Infrastructure.Repositories
{
   public class EntitiesRepository : IEntitiesRepository
    {
        private readonly ApplicationContext bd;

        public EntitiesRepository(ApplicationContext bd)
        {
            this.bd = bd;
        }

        public async Task<bool> AddAsync<T>(T data) where T : Entity
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

        public  Task<T> FindAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] include) where T : Entity
        {
            return Task.FromResult( Include<T>(include).FirstOrDefault(specification.Expression));
        }

        public Task<IEnumerable<T>> GetAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] include) where T : Entity
        {
            
            return Task.FromResult(Include<T>(include).Where(specification.Expression).AsEnumerable());
        }

        public  Task<IQueryable<T>> GetQueryAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] include) where T : Entity
        {
            return Task.FromResult(Include<T>(include).Where(specification.Expression));
        }

        public async Task<bool> RemoveAsync<T>(T data) where T : Entity
        {
            try
            {
                bd.Remove(data);
                await bd.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> RemoveAsync<T>(int id) where T : Entity
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

        public async Task<bool> UpdateAsync<T>(T data) where T : Entity
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

        //IQueryable<T> Include<T>( Func<IQueryable<T>,IIncludableQueryable<T, object>> include ) where T : Entity
        //{
        //    IQueryable<T> query = bd.Set<T>().AsQueryable();
        //    if (include != null)
        //    {
        //        query = include(query);
        //    }
        //    return query;
        //}

        //void Foo()
        //{

        //    // Attribute work 
        //    //Func<IQueryable<User>, IQueryable<User>> includes = DbContextHelper.GetNavigations<User>();
        //    //IQueryable<User> query = bd.Set<User>();
        //    //if (includes != null)
        //    //{
        //    //    query = includes(query);
        //    //}
        //    //var User = query.FirstOrDefault();
        //    //User.Skills.Count();
        //    // EF6?
        //    var user = bd.Set<User>().IncludeEF6(i => i.Skills.Select(j => j.Skill)).FirstOrDefault();
        //    user.Skills.Count();

        //}

        IQueryable<T> Include<T>(params Expression<Func<T, object>>[] includes) where T: Entity
        {
            return bd.Set<T>().AsQueryable().IncludeEF6(includes);
        }

    }
}
