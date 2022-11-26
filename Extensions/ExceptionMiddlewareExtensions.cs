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
            AppException? exception = context.Features.Get<IExceptionHandlerFeature>().Error as AppException;
            return Results.Problem(type: exception?.Type, statusCode: exception?.Status, title: exception?.Title, detail: exception?.Detail, instance: exception?.Instance);
        });
    }
};