using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace QA.Workflow.Exception
{
    public class ApiError : System.Exception
	{
		public string message { get; set; }
		public bool isError { get; set; }
		public string detail { get; set; }
		public IEnumerable<Error> errors { get; set; }

		public ApiError(string message)
		{
			this.message = message;
			isError = true;
		}

		public ApiError(ModelStateDictionary modelState)
		{
			this.isError = true;
			if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
			{
				errors = modelState.Select(m => new Error() { Key = m.Key, ErrorMessage = m.Value.ToString() });
			}
		}
	}
}
