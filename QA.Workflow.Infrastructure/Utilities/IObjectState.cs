using System.ComponentModel.DataAnnotations.Schema;

namespace QR.Workflow.Infrastructure.Utilities
{
    public interface IObjectState
	{
		[NotMapped]
		ObjectState ObjectState { get; set; }
	}
}
