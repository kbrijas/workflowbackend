using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net;

namespace QA.Workflow.Exception
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
	{
        public string _device { get; set; }
        public ApiExceptionFilter(string device)
        {
            _device = device;
        }
       
        public override void OnException(ExceptionContext context)
		{
            ApiError apiError = null;
			if (context.Exception is ApiException)
			{
				var ex = context.Exception as ApiException;
				context.Exception = null;
				apiError = new ApiError(ex.Message);
				apiError.errors = ex.Errors;
				context.HttpContext.Response.StatusCode = ex.StatusCode;
			}
			else if (context.Exception is UnauthorizedAccessException)
			{
				apiError = new ApiError("Unauthorized Access");
				context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
			}
			else
			{
				// Unhandled errors
				#if !DEBUG
					var msg = "An unhandled error occurred.";                
					string stack = null;
				#else
					var msg = context.Exception.GetBaseException().Message;
					string stack = context.Exception.StackTrace;
				#endif

				apiError = new ApiError(msg);
				apiError.detail = stack;
				context.HttpContext.Response.StatusCode = 500;
			}
			context.Result = new JsonResult($"<h1>Error: Bad Request</h1>");
			base.OnException(context);
		}
	}
}
