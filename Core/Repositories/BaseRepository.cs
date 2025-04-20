using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class Repository<TEntity> : IRepository<TEntity>, IDisposable, IAsyncDisposable where TEntity : BaseEntity
{
	protected ApplicationDbContext DbContext { get; }
	protected DbSet<TEntity> Set { get; }
	public IQueryable<TEntity> Items => Set.AsQueryable().AsNoTracking();

	public Repository(ApplicationDbContext dbContext)
	{
		DbContext = dbContext;
		Set = dbContext.Set<TEntity>();
	}

	public async Task<TEntity> CreateAsync(TEntity entity)
	{
		Set.Add(entity);
		await DbContext.SaveChangesAsync();
		DbContext.Entry(entity).State = EntityState.Detached;
		return entity;
	}

	public async Task<TEntity> GetAsync(long id) =>
		await Set.FindAsync(id) ?? throw new ArgumentException($"Entity with id = {id} not found.");

	public async Task<TEntity> UpdateAsync(TEntity entity)
	{
		Set.Update(entity);
		await DbContext.SaveChangesAsync();
		return entity;
	}

	public async Task DeleteAsync(TEntity entity)
	{
		Set.Remove(entity);
		await DbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(long id)
	{
		Set.Remove(await Set.FindAsync(id) ?? throw new InvalidOperationException($"Entity с id = {id} не существует"));
		await DbContext.SaveChangesAsync();
	}

	public void Dispose()
	{
		DbContext.Dispose();
	}

	public async ValueTask DisposeAsync()
	{
		await DbContext.DisposeAsync();
	}

	public async Task UpdateRange(IEnumerable<TEntity> entities)
	{
		if (entities == null || !entities.Any())
		{
			return;
		}

		foreach (var entity in entities)
		{
			DbContext.Entry(entity).State = EntityState.Modified;
		}

		await DbContext.SaveChangesAsync();
	}

	public async Task CreateRange(IEnumerable<TEntity> entities)
	{
		if (entities == null || !entities.Any())
		{
			return;
		}

		await DbContext.Set<TEntity>().AddRangeAsync(entities);

		await DbContext.SaveChangesAsync();
	}
}