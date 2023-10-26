using System;
using System.Linq.Expressions;

namespace Template.Helpers
{
	public static class Helper
	{
        public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string propertyName, bool isAscending)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var lambda = Expression.Lambda(property, parameter);

            string methodName = isAscending ? "OrderBy" : "OrderByDescending";
            Type[] types = new Type[] { query.ElementType, property.Type };

            var methodCall = Expression.Call(typeof(Queryable), methodName, types, query.Expression, Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(methodCall);
        }
    }
}

