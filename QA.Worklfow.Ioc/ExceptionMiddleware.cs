using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using QA.Workflow.Exception;
using System.Net;
using System.Threading.Tasks;

namespace QA.Framework.Exception
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var ex = context.Features.Get<IExceptionHandlerFeature>();
            if (ex.Error is ApiException)
            {
                var error = ex.Error as ApiException;
                context.Response.StatusCode = error.StatusCode;
                context.Response.ContentType = "text/html";
                var err = $"Error:Bad Request";
                await context.Response.WriteAsync(err).ConfigureAwait(false);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/html";
                if (ex != null)
                {
                    var err = $"Error:Bad Request";
                    await context.Response.WriteAsync(err).ConfigureAwait(false);
                }
            }
        }
    }

    public static class exceptionExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }

}
