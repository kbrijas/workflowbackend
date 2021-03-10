using Microsoft.EntityFrameworkCore;
using QR.Workflow.Infrastructure.Utilities;
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.DataContext
{
    public partial class DataContext : DbContext, IDataContext
	{
		#region Private Fields
		private readonly Guid _instanceId;
		bool _disposed;
		#endregion Private Fields

		public DataContext(DbContextOptions options) : base(options)
		{
			_instanceId = Guid.NewGuid();
		}

		public Guid InstanceId { get { return _instanceId; } }
		//
		// Summary:
		//     Saves all changes made in this context to the database.
		//
		// Returns:
		//     The number of state entries written to the database.
		//
		// Exceptions:
		//   T:Microsoft.EntityFrameworkCore.DbUpdateException:
		//     An error is encountered while saving to the database.
		//
		//   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
		//     A concurrency violation is encountered while saving to the database. A concurrency
		//     violation occurs when an unexpected number of rows are affected during save.
		//     This is usually because the data in the database has been modified since it was
		//     loaded into memory.
		//
		// Remarks:
		//     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
		//     to discover any changes to entity instances before saving to the underlying database.
		//     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
		//		Object state also modified to support multiple database if required
		public override int SaveChanges()
		{
			var changes = base.SaveChanges();
			return changes;
		}
		//
		// Summary:
		//     Saves all changes made in this context to the database.
		//
		// Parameters:
		//   acceptAllChangesOnSuccess:
		//     Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
		//     is called after the changes have been sent successfully to the database.
		//
		// Returns:
		//     The number of state entries written to the database.
		//
		// Exceptions:
		//   T:Microsoft.EntityFrameworkCore.DbUpdateException:
		//     An error is encountered while saving to the database.
		//
		//   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
		//     A concurrency violation is encountered while saving to the database. A concurrency
		//     violation occurs when an unexpected number of rows are affected during save.
		//     This is usually because the data in the database has been modified since it was
		//     loaded into memory.
		//
		// Remarks:
		//     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
		//     to discover any changes to entity instances before saving to the underlying database.
		//     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
		//		Object state also modified to support multiple database if required
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			var changes = base.SaveChanges(acceptAllChangesOnSuccess);
			return changes;
		}
		//
		// Summary:
		//     Asynchronously saves all changes made in this context to the database.
		//
		// Parameters:
		//   acceptAllChangesOnSuccess:
		//     Indicates whether Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AcceptAllChanges
		//     is called after the changes have been sent successfully to the database.
		//
		//   cancellationToken:
		//     A System.Threading.CancellationToken to observe while waiting for the task to
		//     complete.
		//
		// Returns:
		//     A task that represents the asynchronous save operation. The task result contains
		//     the number of state entries written to the database.
		//
		// Exceptions:
		//   T:Microsoft.EntityFrameworkCore.DbUpdateException:
		//     An error is encountered while saving to the database.
		//
		//   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
		//     A concurrency violation is encountered while saving to the database. A concurrency
		//     violation occurs when an unexpected number of rows are affected during save.
		//     This is usually because the data in the database has been modified since it was
		//     loaded into memory.
		//
		// Remarks:
		//     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
		//     to discover any changes to entity instances before saving to the underlying database.
		//     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
		//     Multiple active operations on the same context instance are not supported. Use
		//     'await' to ensure that any asynchronous operations have completed before calling
		//     another method on this context.
		//		Object state also modified to support multiple database if required
		public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			var changesAsync = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
			return changesAsync;
		}
		//
		// Summary:
		//     Asynchronously saves all changes made in this context to the database.
		//
		// Parameters:
		//   cancellationToken:
		//     A System.Threading.CancellationToken to observe while waiting for the task to
		//     complete.
		//
		// Returns:
		//     A task that represents the asynchronous save operation. The task result contains
		//     the number of state entries written to the database.
		//
		// Exceptions:
		//   T:Microsoft.EntityFrameworkCore.DbUpdateException:
		//     An error is encountered while saving to the database.
		//
		//   T:Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException:
		//     A concurrency violation is encountered while saving to the database. A concurrency
		//     violation occurs when an unexpected number of rows are affected during save.
		//     This is usually because the data in the database has been modified since it was
		//     loaded into memory.
		//
		// Remarks:
		//     This method will automatically call Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.DetectChanges
		//     to discover any changes to entity instances before saving to the underlying database.
		//     This can be disabled via Microsoft.EntityFrameworkCore.ChangeTracking.ChangeTracker.AutoDetectChangesEnabled.
		//     Multiple active operations on the same context instance are not supported. Use
		//     'await' to ensure that any asynchronous operations have completed before calling
		//     another method on this context.
		//		Object state also modified to support multiple database if required
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			var changesAsync = await base.SaveChangesAsync(cancellationToken);
			return changesAsync;
		}
		/// <summary>
		/// Sync Object state
		/// </summary>
		/// <typeparam name="TEntity"></typeparam>
		/// <param name="entity"></param>
		public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
		{
			Entry(entity).State = ObjectStateHelper.ConvertState(entity.ObjectState);
		}
		/// <summary>
		/// Sync Object state to entity state
		/// </summary>
		private void SyncObjectsStatePreCommit()
		{
			foreach (var dbEntityEntry in ChangeTracker.Entries())
			{
				dbEntityEntry.State = ObjectStateHelper.ConvertState(((IObjectState)dbEntityEntry.Entity).ObjectState);
			}
		}
		/// <summary>
		/// Sync entity state to object state
		/// </summary>
		public void SyncObjectsStatePostCommit()
		{
			foreach (var dbEntityEntry in ChangeTracker.Entries())
			{
				((IObjectState)dbEntityEntry.Entity).ObjectState = ObjectStateHelper.ConvertState(dbEntityEntry.State);
			}
		}

		public void BeginTransaction(DbTransaction transaction)
		{
			if (null == transaction)
			{
				if (base.Database.GetDbConnection().State != ConnectionState.Open)
				{
					base.Database.OpenConnection();
				}
				transaction = (DbTransaction)base.Database.CurrentTransaction;
				base.Database.UseTransaction(transaction);
			}
		}
		public void Dispose(bool disposing)
		{
			if (_disposed)
				return;
			if (disposing)
			{
				try
				{
					if (base.Database.GetDbConnection().State == ConnectionState.Open)
					{
						base.Database.GetDbConnection().Close();
					}
				}
				catch (ObjectDisposedException)
				{
				}
				if (this != null)
				{
					this.Dispose();
				}
			}
			_disposed = true;
		}
	}
}
