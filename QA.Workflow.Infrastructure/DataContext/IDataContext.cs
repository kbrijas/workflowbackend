using QR.Workflow.Infrastructure.Utilities;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.DataContext
{
    public interface IDataContext : IDisposable
	{
		//
		// Summary:
		//     Saves all changes made in this context to the database.
		//
		int SaveChanges();
		//
		// Summary:
		//     Saves all changes made in this context to the database.
		//
		int SaveChanges(bool acceptAllChangesOnSuccess);
		//
		// Summary:
		//     Asynchronously saves all changes made in this context to the database.
		//
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
		//
		// Summary:
		//     Asynchronously saves all changes made in this context to the database.
		//
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
		//void BeginTransaction(DbTransaction transaction, IsolationLevel isolationLevel = IsolationLevel.Unspecified);
		void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
		void SyncObjectsStatePostCommit();
		void Dispose(bool disposing);
		void BeginTransaction(DbTransaction transaction);
	}
}
