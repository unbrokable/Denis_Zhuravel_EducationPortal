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
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Model;
using Infrastructure.Extensions;
using System.Linq.Dynamic.Core;

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
            return Task.FromResult( Include<T>(null,include).FirstOrDefault(specification.Expression));
        }

        public Task<IEnumerable<T>> GetAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] include) where T : Entity
        {
            
            return Task.FromResult(Include<T>(specification,include).AsEnumerable<T>());
        }

        public async Task<PageList<T>> GetAsync<T>(int pageNumber, int pageSize, Specification<T> specification = null, params Expression<Func<T, object>>[] include) where T : Entity
        {
            return await Include<T>(specification, include).ToPageListAsync(pageNumber, pageSize);
        }

        public Task<IEnumerable<R>> GetCustomSelectAsync<T,R>(Specification<T> specification, IConfigurationProvider configuration, params Expression<Func<T, object>>[] includes) where T : Entity
        {
            return Task.FromResult(Include<T>(specification,includes).ProjectTo<R>(configuration).AsEnumerable<R>());
        }

        public  Task<IQueryable<T>> GetQueryAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] include) where T : Entity
        {
            return Task.FromResult(Include<T>(specification,include));
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

        IQueryable<T> Include<T>(Specification<T> specification , params Expression<Func<T, object>>[] includes) where T: Entity
        {
            if (specification == null)
            {
                return bd.Set<T>().AsQueryable().IncludeEF6(includes);
            }
            return bd.Set<T>().AsQueryable().IncludeEF6(includes).Where(specification.Expression);
        }
    }
}
