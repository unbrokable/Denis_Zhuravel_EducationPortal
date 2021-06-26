using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extantion
{
    static  class QueryableExtensions
    {
        internal static IQueryable<T> IncludeEF6<T>(this IQueryable<T> source, params Expression<Func<T, object>>[] paths) where T: class
        {
            if (paths != null)
                source = paths.Aggregate(source, (current, include) => current.Include(include.AsPath()));

            return source;
        }
    }
}
