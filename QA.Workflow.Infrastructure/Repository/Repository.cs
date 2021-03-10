using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using QR.Workflow.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace QR.Workflow.Infrastructure.Repository
{
    public class Repository<TEntity>: IRepository<TEntity> where TEntity : class
	{
		protected readonly IDataContext _context;
		protected readonly DbSet<TEntity> _dbSet;
		protected readonly DbContext _dbContext;
		public Repository(IDataContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_dbContext = _context as DbContext;
			_dbSet = _dbContext.Set<TEntity>();
		}
		/// <summary>
		/// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
		/// </summary>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="orderBy">A function to order elements.</param>
		/// <param name="include">A function to include navigation properties</param>
		/// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
		/// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
		/// <remarks>This method default no-tracking query.</remarks>
		public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate = null,
										 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
										 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
										 bool disableTracking = true)
		{
			IQueryable<TEntity> query = _dbSet;
			if (disableTracking)
			{
				query = query.AsNoTracking();
			}

			if (include != null)
			{
				query = include(query);
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				return orderBy(query).FirstOrDefault();
			}
			else
			{
				return query.FirstOrDefault();
			}
		}

		/// <summary>
		/// Gets the first or default entity based on a predicate, orderby delegate and include delegate. This method default no-tracking query.
		/// </summary>
		/// <param name="selector">The selector for projection.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <param name="orderBy">A function to order elements.</param>
		/// <param name="include">A function to include navigation properties</param>
		/// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
		/// <returns>An <see cref="IPagedList{TEntity}"/> that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
		/// <remarks>This method default no-tracking query.</remarks>
		public TResult GetFirstOrDefault<TResult>(Expression<Func<TEntity, TResult>> selector,
												  Expression<Func<TEntity, bool>> predicate = null,
												  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
												  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
												  bool disableTracking = true)
		{
			IQueryable<TEntity> query = _dbSet;
			if (disableTracking)
			{
				query = query.AsNoTracking();
			}

			if (include != null)
			{
				query = include(query);
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (orderBy != null)
			{
				return orderBy(query).Select(selector).FirstOrDefault();
			}
			else
			{
				return query.Select(selector).FirstOrDefault();
			}
		}

		/// <summary>
		/// Uses raw SQL queries to fetch the specified <typeparamref name="TEntity" /> data.
		/// </summary>
		/// <param name="sql">The raw SQL.</param>
		/// <param name="parameters">The parameters.</param>
		/// <returns>An <see cref="IQueryable{TEntity}" /> that contains elements that satisfy the condition specified by raw SQL.</returns>
		public IQueryable<TEntity> FromSql(string sql, params object[] parameters) => _dbSet.FromSqlRaw(sql, parameters);

		/// <summary>
		/// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
		/// </summary>
		/// <param name="keyValues">The values of the primary key for the entity to be found.</param>
		/// <returns>The found entity or null.</returns>
		public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);

		/// <summary>
		/// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
		/// </summary>
		/// <param name="keyValues">The values of the primary key for the entity to be found.</param>
		/// <returns>A <see cref="Task{TEntity}" /> that represents the asynchronous insert operation.</returns>
		public async Task<TEntity> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);

		/// <summary>
		/// Finds an entity with the given primary key values. If found, is attached to the context and returned. If no entity is found, then null is returned.
		/// </summary>
		/// <param name="keyValues">The values of the primary key for the entity to be found.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A <see cref="Task{TEntity}"/> that represents the asynchronous find operation. The task result contains the found entity or null.</returns>
		public async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken) => await _dbSet.FindAsync(keyValues, cancellationToken);

		/// <summary>
		/// Gets the count based on a predicate.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public int Count(Expression<Func<TEntity, bool>> predicate = null)
		{
			if (predicate == null)
			{
				return _dbSet.Count();
			}
			else
			{
				return _dbSet.Count(predicate);
			}
		}

		/// <summary>
		/// Inserts a new entity synchronously.
		/// </summary>
		/// <param name="entity">The entity to insert.</param>
		public void Insert(TEntity entity)
		{
			var entry = _dbSet.Add(entity);
		}

		/// <summary>
		/// Inserts a range of entities synchronously.
		/// </summary>
		/// <param name="entities">The entities to insert.</param>
		public void Insert(params TEntity[] entities) => _dbSet.AddRange(entities);

		/// <summary>
		/// Inserts a range of entities synchronously.
		/// </summary>
		/// <param name="entities">The entities to insert.</param>
		public void Insert(IEnumerable<TEntity> entities) => _dbSet.AddRange(entities);

		/// <summary>
		/// Inserts a new entity asynchronously.
		/// </summary>
		/// <param name="entity">The entity to insert.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
		public Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
		{
			return _dbSet.AddAsync(entity, cancellationToken).AsTask();
		}

		/// <summary>
		/// Inserts a range of entities asynchronously.
		/// </summary>
		/// <param name="entities">The entities to insert.</param>
		/// <returns>A <see cref="Task" /> that represents the asynchronous insert operation.</returns>
		public Task InsertAsync(params TEntity[] entities) => _dbSet.AddRangeAsync(entities);

		/// <summary>
		/// Inserts a range of entities asynchronously.
		/// </summary>
		/// <param name="entities">The entities to insert.</param>
		/// <param name="cancellationToken">A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
		/// <returns>A <see cref="Task"/> that represents the asynchronous insert operation.</returns>
		public Task InsertAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken)) => _dbSet.AddRangeAsync(entities, cancellationToken);

		/// <summary>
		/// Updates the specified entity.
		/// </summary>
		/// <param name="entity">The entity.</param>
		public void Update(TEntity entity)
		{
			_dbSet.Update(entity);
		}

		/// <summary>
		/// Updates the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		public void Update(params TEntity[] entities) => _dbSet.UpdateRange(entities);

		/// <summary>
		/// Updates the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		public void Update(IEnumerable<TEntity> entities) => _dbSet.UpdateRange(entities);

		/// <summary>
		/// Deletes the specified entity.
		/// </summary>
		/// <param name="entity">The entity to delete.</param>
		public void Delete(TEntity entity) => _dbSet.Remove(entity);

		/// <summary>
		/// Deletes the entity by the specified primary key.
		/// </summary>
		/// <param name="id">The primary key value.</param>
		public void Delete(object id)
		{
			// using a stub entity to mark for deletion
			var typeInfo = typeof(TEntity).GetTypeInfo();
			var key = _dbContext.Model.FindEntityType(typeInfo.Name).FindPrimaryKey().Properties.FirstOrDefault();
			var property = typeInfo.GetProperty(key?.Name);
			if (property != null)
			{
				var entity = Activator.CreateInstance<TEntity>();
				property.SetValue(entity, id);
				_dbContext.Entry(entity).State = EntityState.Deleted;
			}
			else
			{
				var entity = _dbSet.Find(id);
				if (entity != null)
				{
					Delete(entity);
				}
			}
		}

		/// <summary>
		/// Deletes the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		public void Delete(params TEntity[] entities) => _dbSet.RemoveRange(entities);

		/// <summary>
		/// Deletes the specified entities.
		/// </summary>
		/// <param name="entities">The entities.</param>
		public void Delete(IEnumerable<TEntity> entities) => _dbSet.RemoveRange(entities);

		public IQueryable<TEntity> Queryable()
		{
			return _dbSet;
		}
		public async Task<List<TEntity>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}
	}
}
