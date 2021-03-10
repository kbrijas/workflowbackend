using System.Collections.Generic;

namespace QA.Workflow.Exception
{
    public class ApiException : System.Exception
	{
		public int StatusCode { get; set; }

		public IEnumerable<Error> Errors { get; set; }

		public ApiException(string message,
							int statusCode = 500,
							IEnumerable<Error> errors = null) :
			base(message)
		{
			StatusCode = statusCode;
			Errors = errors;
		}
		public ApiException(System.Exception ex, int statusCode = 500) : base(ex.Message)
		{
			StatusCode = statusCode;
		}
	}
}
