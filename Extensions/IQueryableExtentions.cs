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
            if (queryObject.IsSortAscending)
            {
                query = query.OrderBy(columnMap[queryObject.SortBy]);
            }
            else
            {
                query = query.OrderByDescending(columnMap[queryObject.SortBy]);
            }

            return query;
        }
    }
}