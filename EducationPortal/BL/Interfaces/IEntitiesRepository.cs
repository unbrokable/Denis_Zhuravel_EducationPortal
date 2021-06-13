using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Application.Interfaces
{
    public  interface IEntitiesRepository
    {
        bool Create<T>(T data) where T : class;
        bool Update<T>(T data) where T : class;
        IEnumerable<T> GetAllBy<T>(Func<T,bool> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
        T GetBy<T>(Func<T, bool> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null) where T : class;
    }
}

