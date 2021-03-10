namespace QR.Workflow.Infrastructure.Repository
{
    public interface IFactoryRepository
	{
		/// <summary>
		/// Gets the specified repository for the <typeparamref name="TEntity"/>.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <returns>An instance of type inherited from <see cref="IRepository{TEntity}"/> interface.</returns>
		IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
	}
}
