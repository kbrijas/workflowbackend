using Microsoft.EntityFrameworkCore;
using QR.Workflow.Infrastructure.DataContext;

namespace QA.Workflow.Data.Context
{
    public class MigrationDataContext: DataContext
	{
		public MigrationDataContext(DbContextOptions options)
			: base(options)
		{ }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			
		}
	}
}
