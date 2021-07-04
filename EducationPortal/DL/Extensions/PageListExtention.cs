using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    static class PageListExtention
    {
        public static async Task<PageList<T>> ToPageListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            if (pageNumber < 0)
            {
                throw new ArgumentOutOfRangeException($"pageNumber = {pageNumber}. PageNumber cannot be below 1.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException($"pageSize = {pageSize}. PageSize cannot be less than 1.");
            }

            var subset = new List<T>();
            long totalCount = 0;
            if (query != null)
            {
                totalCount = await query.LongCountAsync();
                if (totalCount > 0)
                {
                    query =  query.Skip((pageNumber * pageSize)).Take(pageSize);
                    subset = await query.ToListAsync();
                }
            }

            return new PageList<T>(pageNumber, pageSize, totalCount, subset);
        }
    }
}
