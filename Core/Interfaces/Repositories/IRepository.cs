﻿namespace Core.Interfaces.Repositories;

public interface IRepository<TEntity>
{
	Task<TEntity> CreateAsync(TEntity entity);
	Task<TEntity> GetAsync(long id);
	Task<TEntity> UpdateAsync(TEntity entity);
	Task DeleteAsync(TEntity entity);
	Task DeleteAsync(long id);
	public IQueryable<TEntity> Items { get; }
}

