using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VegaSPA.Core.Models;

namespace VegaSPA.Extensions
{
    public static class IQueryableExtentions
    {
        private const byte PAGE_SIZE = 10;
        private const byte PAGE_NUMBER = 1;
        
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (String.IsNullOrWhiteSpace(queryObject.SortBy))
            {
                return query;
            }

            if (!columnMap.ContainsKey(queryObject.SortBy))
            {
                return query;
            }

            if (queryObject.IsSortAscending)
            {
                return query = query.OrderBy(columnMap[queryObject.SortBy]);
            }
            else
            {
                return query = query.OrderByDescending(columnMap[queryObject.SortBy]);
            }
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if(!queryObject.Page.HasValue || !queryObject.PageSize.HasValue)
            {
                return query;
            }

            if (queryObject.PageSize <= 0)
            {
                queryObject.PageSize = PAGE_SIZE;
            }

            if (queryObject.Page <= 0)
            {
                queryObject.Page = PAGE_NUMBER;
            }

            return query
                .Skip((queryObject.Page.Value - 1) * queryObject.PageSize.Value)
                .Take(queryObject.PageSize.Value);
        }
    }
}