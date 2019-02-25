using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using VegaSPA.Core.Models;

namespace VegaSPA.Extensions
{
    public static class IQueryableExtentions
    {
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
    }
}