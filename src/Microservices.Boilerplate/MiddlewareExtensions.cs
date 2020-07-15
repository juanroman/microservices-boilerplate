using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Boilerplate
{
    /// <summary>
    /// Defines extensions methods for the middleware services.
    /// </summary>
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Instructs the <see cref="IApplicationBuilder"/> to use the <see cref="ExceptionMiddleware"/> service.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> to extend.</param>
        public static void UseMiddlewareExceptionHandling(this IApplicationBuilder app) => app.UseMiddleware<ExceptionMiddleware>();

        /// <summary>
        /// Instructs the <see cref="IServiceCollection"/> to use the <see cref="ModelValidationAttribute"/> attribute.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to extend.</param>
        public static void AddModelValidation(this IServiceCollection services) => services.AddScoped<ModelValidationAttribute>();
    }
}
