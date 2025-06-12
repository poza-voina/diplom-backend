using Core.Interfaces.Repositories;
using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories;

public class RouteRepository(ApplicationDbContext dbContext) : Repository<Route>(dbContext), IRouteRepository
{
	public async Task<Route> UpdateRelationShips(Route entity)
	{
		var existingCategoriesIds = DbContext.RouteRouteCategories
			.AsQueryable()
			.Where(x => entity.Id == x.RouteId);

		DbContext.RemoveRange(existingCategoriesIds);

		var newCategories = entity.RouteCategories
			.Select(x => new RouteRouteCategory { RouteId = entity.Id!.Value, RouteCategoryId = x.Id!.Value });

		DbContext.RouteRouteCategories.AddRange(newCategories);

		await DbContext.SaveChangesAsync();
		
		return entity;
	}
}
