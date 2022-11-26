using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Entities;
using Ecommerce.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Ecommerce.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.Map("/error", (HttpContext context) =>
        {
            var exception = context.Features.Get<IExceptionHandlerFeature>().Error;
            if (exception is AppException)
            {
                AppException appException = exception as AppException;
                return Results.Problem(type: appException?.Type, statusCode: appException?.Status, title: appException?.Title, detail: appException?.Detail, instance: appException?.Instance);
            }

            return Results.Problem(statusCode: (int)HttpStatusCode.InternalServerError, detail: exception?.ToString());
        });
    }
};