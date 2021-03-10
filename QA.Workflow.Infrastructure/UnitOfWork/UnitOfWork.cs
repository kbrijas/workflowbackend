using Dapper;
using QR.Workflow.Infrastructure.Dapper;
using QR.Workflow.Infrastructure.DataContext;
using QR.Workflow.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IFactoryRepository, IUnitOfWork
	{
		private readonly Dictionary<Type, dynamic> _repositories;
		private readonly IFactoryRepository _factory;
		private readonly IDataContext _context;
		private readonly IDapperExtension _dapperExtension;
		private DbTransaction _transaction;
		public UnitOfWork(IDataContext context, IDapperExtension dapperExtension)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_dapperExtension = dapperExtension ?? throw new ArgumentNullException(nameof(dapperExtension));
		}

		public IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class
		{
			throw new NotImplementedException();
		}

		public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
		{
			var type = typeof(TEntity);
			if (_repositories.ContainsKey(type))
			{
				return (IRepository<TEntity>)_repositories[type];
			}
			var repositoryType = typeof(Repository<>);

			_repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context, this));

			return _repositories[type];
		}

		public int SaveChanges()
		{
			return _context.SaveChanges();
		}

		public int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			return _context.SaveChanges(acceptAllChangesOnSuccess);
		}

		public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _context.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			return _context.SaveChangesAsync(cancellationToken);
		}
		public void BeginTransaction()
		{
			_context.BeginTransaction(_transaction);
		}
		public void Commit() {
			_transaction.Commit();
		}
		public void Rollback()
		{
			_transaction.Rollback();
		}
		#region IDisposable Support
		public void Dispose()
		{
			_context.Dispose(true);
			GC.SuppressFinalize(this);
		}

		public async Task<List<T>> ExecuteReaderAsync<T>(string sqlQuery, DynamicParameters parameterValues)
		{
			return await _dapperExtension.ExecuteReaderAsync<T>(sqlQuery, parameterValues);
		}

		public List<T> ExecuteReader<T>(string sqlQuery, DynamicParameters parameterValues)
		{
			return _dapperExtension.ExecuteReader<T>(sqlQuery, parameterValues);
		}

		public async Task<List<T>> ExecuteNonQueryReaderAsync<T>(string storedProcedureName, DynamicParameters parameterValues)
		{
			return await _dapperExtension.ExecuteNonQueryReaderAsync<T>(storedProcedureName, parameterValues);
		}

		public List<T> ExecuteNonQueryReader<T>(string storedProcedureName, DynamicParameters parameterValues)
		{
			return _dapperExtension.ExecuteNonQueryReader<T>(storedProcedureName, parameterValues);
		}

		public async Task<List<T>> ExecuteNonQueryAsync<T>(string storedProcedureName, DynamicParameters parameterValues)
		{
			return await _dapperExtension.ExecuteNonQueryAsync<T>(storedProcedureName, parameterValues);
		}

		public List<T> ExecuteNonQuery<T>(string storedProcedureName, DynamicParameters parameterValues)
		{
			return _dapperExtension.ExecuteNonQuery<T>(storedProcedureName, parameterValues);
		}
		#endregion
	}
}
