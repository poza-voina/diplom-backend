using Core.Entities;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Repositories;

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
		ArgumentNullException.ThrowIfNull(entity);

		var trackedEntity = DbContext.Set<TEntity>()
			.Local
			.FirstOrDefault(e => e.Id == entity.Id);

		if (trackedEntity is { })
		{
			DbContext.Entry(trackedEntity).State = EntityState.Detached;
		}
		else if (DbContext.Entry(entity).State is EntityState.Detached)
		{
			DbContext.Entry(entity).State = EntityState.Modified;
		}

		DbContext.Update(entity);
		await DbContext.SaveChangesAsync();
		DbContext.Entry(entity).State = EntityState.Detached;

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
}