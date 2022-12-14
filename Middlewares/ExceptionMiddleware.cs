// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using Ecommerce.Entities;

// namespace Ecommerce.Middlewares;
// public class ExceptionMiddleware
// {
//     private readonly RequestDelegate _next;

//     public ExceptionMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task InvokeAsync(HttpContext httpContext)
//     {
//         try
//         {
//             await _next(httpContext);
//         }
//         catch (System.Exception ex)
//         {
//             await HandleExceptionAsync(httpContext, ex);
//             throw;
//         }
//     }

//     private async Task HandleExceptionAsync(HttpContext context, Exception exception)
//     {
//         context.Response.ContentType = "application/json";
//         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//         await context.Response.WriteAsync(new ErrorDetails()
//         {
//             StatusCode = context.Response.StatusCode,
//             Message = "Internal Server Error from the custom middleware."
//         }.ToString());
//     }
// }