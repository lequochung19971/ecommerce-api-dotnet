using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecommerce.Extensions;

namespace Ecommerce.Common.Helpers
{
    public class ExpressionHelpers
    {

        public static Expression<Func<T, bool>> Ors<T>(Expression<Func<T, bool>>[] expressions)
        {
            Expression<Func<T, bool>> result = null;
            foreach (var expression in expressions)
            {
                if (result == null)
                {
                    result = expression;
                    continue;
                }
                result = result.Or(expression);
            }
            return result;
        }
    }
}