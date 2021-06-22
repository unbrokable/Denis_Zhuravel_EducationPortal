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
        Task<bool> AddAsync<T>(T data) where T : class;
        Task<bool> UpdateAsync<T>(T data) where T : class;
        Task<IEnumerable<T>> GetAsync<T>(Specification<T> specification, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
        Task<IQueryable<T>> GetQueryAsync<T>(Specification<T> specification, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
        Task<T> FindAsync<T>(Specification<T> specification, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
        Task<bool> RemoveAsync<T>(T data) where T : class;
        Task<bool> RemoveAsync<T>(int key) where T : class;
    }
}

