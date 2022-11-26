using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Ecommerce.Extensions;

public static class ExpressionExceptions
{
    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var body = Expression.Or(
                Expression.Invoke(left, param),
                Expression.Invoke(right, param)
            );
        var lambda = Expression.Lambda<Func<T, bool>>(body, param);
        return lambda;
    }
}