using Microsoft.EntityFrameworkCore;

namespace Core.Extensions;

public static class QueryableExtensions
{
	public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageNumber, int pageSize)
	{
		return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
	}

	public static IQueryable<TEntity> IncludeAllCollections<TEntity>(this DbSet<TEntity> dbSet, DbContext context)
		where TEntity : class
	{
		var entityType = context.Model.FindEntityType(typeof(TEntity));
		var navigationProperties = entityType
			.GetNavigations()
			.Where(n => n.IsCollection)
			.Select(n => n.Name);

		IQueryable<TEntity> query = dbSet;

		foreach (var navigationProperty in navigationProperties)
		{
			query = query.Include(navigationProperty);
		}

		return query;
	}
}