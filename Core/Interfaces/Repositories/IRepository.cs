namespace Core.Interfaces.Repositories;

public interface IRepository<TEntity>
{
	Task<TEntity> CreateAsync(TEntity entity);
	Task<TEntity> GetAsync(long id);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task DeleteAsync(TEntity entity);
	Task DeleteAsync(long id);
	Task<IEnumerable<TEntity>> UpdateRangeAsync(IEnumerable<TEntity> entities);
	Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
	IQueryable<TEntity> Items { get; }
	
}

