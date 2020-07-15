using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Microservices.Boilerplate
{
    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> that verifies that <see cref="ModelStateDictionary.IsValid"/> is <c>true</c>; otherwise, returns a 404/Bad-Request result immediately.
    /// </summary>
    /// <seealso cref="ActionFilterAttribute" />
    public class ModelValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called by the ASP.NET MVC framework before the action method executes.
        /// </summary>
        /// <param name="context"><see cref="ActionExecutingContext"/></param>
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
