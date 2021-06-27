using AutoMapper;
using Domain.Entities;
using Domain.Model;
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
       
        Task<IEnumerable<T>> GetAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] includes ) where T : Entity;
        Task<PageList<T>> GetAsync<T>( int pageNumber, int pageSize, Specification<T> specification = null, params Expression<Func<T, object>>[] includes) where T : Entity;
       
        Task<IEnumerable<R>> GetCustomSelectAsync<T,R>(Specification<T> specification, IConfigurationProvider configuration, params Expression<Func<T, object>>[] includes) where T : Entity;
        
        Task<T> FindAsync<T>(Specification<T> specification, params Expression<Func<T, object>>[] includes) where T : Entity;
       
        Task<bool> RemoveAsync<T>(T data) where T : Entity;
        Task<bool> RemoveAsync<T>(int key) where T : Entity;
        Task<bool> AddAsync<T>(T data) where T : Entity;
        Task<bool> UpdateAsync<T>(T data) where T : Entity;
    }
}

