using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
	{
		/// <summary>
		/// Save changes
		/// </summary>
		/// <returns></returns>
		int SaveChanges();
		/// <summary>
		/// Save changes
		/// </summary>
		/// <param name="acceptAllChangesOnSuccess"></param>
		/// <returns></returns>
		int SaveChanges(bool acceptAllChangesOnSuccess);
		/// <summary>
		/// save changes async
		/// </summary>
		/// <param name="acceptAllChangesOnSuccess"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
		/// <summary>
		/// sanve changes async
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
		/// <summary>
		/// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity"/> data.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="sql">The raw SQL.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>An <see cref="IQueryable{T}"/> that contains elements that satisfy the condition specified by raw SQL.</returns>
		IQueryable<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : class;
		/// <summary>
		/// Begin Transaction
		/// </summary>
		/// <param name="isolationLevel"></param>
		void BeginTransaction();
		/// <summary>
		/// Transaction commit
		/// </summary>
		/// <returns></returns>
		void Commit();
		/// <summary>
		/// Transaction rollback
		/// </summary>
		/// <returns></returns>
		void Rollback();
		/// <summary>
		/// ExecuteReaderAsync
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sqlQuery"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		Task<List<T>> ExecuteReaderAsync<T>(string sqlQuery, DynamicParameters parameterValues);
		/// <summary>
		/// ExecuteReader
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="sqlQuery"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		List<T> ExecuteReader<T>(string sqlQuery, DynamicParameters parameterValues);
		/// <summary>
		/// ExecuteNonQueryReaderAsync
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		Task<List<T>> ExecuteNonQueryReaderAsync<T>(string storedProcedureName, DynamicParameters parameterValues);
		/// <summary>
		/// ExecuteNonQueryReader
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		List<T> ExecuteNonQueryReader<T>(string storedProcedureName, DynamicParameters parameterValues);
		/// <summary>
		/// ExecuteNonQueryAsync
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		Task<List<T>> ExecuteNonQueryAsync<T>(string storedProcedureName, DynamicParameters parameterValues);
		/// <summary>
		/// ExecuteNonQuery
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="storedProcedureName"></param>
		/// <param name="parameterValues"></param>
		/// <returns></returns>
		List<T> ExecuteNonQuery<T>(string storedProcedureName, DynamicParameters parameterValues);
	}
}
