using GestionNotification.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestionNotification.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // Gestion spécifique de NotificationNotFoundException
            context.Response.StatusCode = exception switch
            {
                NotificationException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var result = JsonSerializer.Serialize(new { error = exception.Message });
            return context.Response.WriteAsync(result);
        }
    }
}