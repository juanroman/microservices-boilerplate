using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservices.Boilerplate
{
    /// <summary>
    /// Defines exception handling middleware that encapsulates exceptions into a Json formatted <see cref="MicroserviceError"/> message.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="requestDelegate">The <see cref="RequestDelegate"/> from dependency injection.</param>
        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        /// <summary>
        /// Executes the <c>next</c> <see cref="RequestDelegate"/> handling any exceptions that might occurr.
        /// </summary>
        /// <param name="httpContext"><see cref="Task"/></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var error = new MicroserviceError
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = exception.Message
            };

            var json = JsonSerializer.Serialize(error);

            return httpContext.Response.WriteAsync(json);
        }
    }
}
