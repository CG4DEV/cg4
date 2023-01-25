using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProjectName.WebApp
{
    public class SupportRestfulApi : IResultFilter
    {
        public static SupportRestfulApi Instance => new();
        
        /// <inheritdoc />
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.ActionDescriptor is ControllerActionDescriptor actionDescriptor)
            {
                var returnType = actionDescriptor.MethodInfo.ReturnType;
                if (returnType == typeof(void) || returnType == typeof(Task))
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    return;
                }

                if (context.Result is ObjectResult { Value: null })
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
        }

        /// <inheritdoc />
        public void OnResultExecuted(ResultExecutedContext context)
        {
            
        }
    }
}