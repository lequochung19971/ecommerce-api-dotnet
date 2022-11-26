using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Ecommerce.Common.Helpers;
using Ecommerce.Entities;
using Ecommerce.Enums;

namespace Ecommerce.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string sortField, SortDirections sortDirection)
    {
        var hasProperty = typeof(T).HasProperty(sortField);
        if (!queryable.Any() || string.IsNullOrWhiteSpace(sortField) || !hasProperty)
        {
            return queryable;
        }

        return queryable.OrderBy($"{sortField} {sortDirection.convertToString()}");
    }

    public static IQueryable<T> SearchByColumns<T>(this IQueryable<T> queryable, string[] columnNames, string searchValue)
    {
        var expressions = CreateSearchExpression<T>(columnNames, searchValue);
        if (!queryable.Any() || string.IsNullOrWhiteSpace(searchValue) || expressions == null)
        {
            return queryable;
        }

        var combinedExpression = ExpressionHelpers.Ors(expressions);
        return queryable.Where(combinedExpression);

    }

    public static IQueryable<T> Paging<T>(this IQueryable<T> queryable, int pageNumber, int pageSize)
    {
        return queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<Object> Selects<T>(this IQueryable<T> queryable, string[] fields)
    {
        var a = CreateNewStatement<T>(fields);
        return queryable.Select(a);
    }

    public async static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryable, int pageNumber, int pageSize, bool requireTotalCount)
    {
        int? count = requireTotalCount ? queryable.Count() : null;
        var items = await queryable.Paging(pageNumber, pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    private static Expression<Func<T, bool>>[] CreateSearchExpression<T>(string[] columnNames, object searchValue)
    {
        var expressions = new List<Expression<Func<T, bool>>>();
        foreach (var columnName in columnNames)
        {
            var expression = CreateExpression<T>(columnName, searchValue);
            expressions.Add(expression);
        };

        return expressions.ToArray();
    }

    private static Expression<Func<T, bool>> CreateExpression<T>(string columnName, object searchValue)
    {
        var xType = typeof(T);
        var x = Expression.Parameter(xType, "x");
        var properties = xType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == columnName);

        MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });

        var body = properties == null
            ? (Expression)Expression.Constant(false)
            : Expression.Call(
                Expression.PropertyOrField(x, columnName),
                containsMethod,
                Expression.Constant(searchValue));

        return Expression.Lambda<Func<T, bool>>(body, x);
    }


    private static Expression<Func<T, Object>> CreateNewStatement<T>(string[] fields)
    {
        var xType = typeof(T);
        var x = Expression.Parameter(xType, "x");
        // var properties = xType.GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault(p => p.Name == columnName);
        // var requiredFields = GetRequiredProperties(fields, properties);
        var body = (Expression)Expression.Constant(new
        {
            Name = Expression.Property(x, "Name"),
            Desc = Expression.Property(x, "Desc")
        }
        );

        return Expression.Lambda<Func<T, Object>>(body, x);
    }

    private static IEnumerable<PropertyInfo> GetRequiredProperties(string[] fields, PropertyInfo[] properties)
    {
        var requiredProperties = new List<PropertyInfo>();

        if (fields != null)
        {
            foreach (var field in fields)
            {
                var property = properties.FirstOrDefault(prop => prop.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));
                if (property == null)
                {
                    continue;
                }
                requiredProperties.Add(property);
            }
        }
        else
        {
            requiredProperties = properties.ToList();
        }

        return requiredProperties;
    }
}