using Domain.Entities;
using Domain.Specification;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public  interface IEntitiesRepository
    {
        Task<bool> AddAsync<T>(T data) where T : Entity;
        Task<bool> UpdateAsync<T>(T data) where T : Entity;
        Task<IEnumerable<T>> GetAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] includes ) where T : Entity;
        Task<IQueryable<T>> GetQueryAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] includes) where T : Entity;
        Task<T> FindAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] includes) where T : Entity;
        Task<bool> RemoveAsync<T>(T data) where T : Entity;
        Task<bool> RemoveAsync<T>(int key) where T : Entity;
    }
}

