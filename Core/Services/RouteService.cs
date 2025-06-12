using System.Linq;
using Application.Controllers;
using Core.Dto;
using Core.Dto.Route;
using Core.Dto.RouteCategory;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class RouteService(IRouteRepository repository) : IRouteService
{
	public async Task<RouteDto> CreateAsync(CreateRouteRequest dto)
	{
		var entity = dto.Adapt<Route>();
		entity = await repository.CreateAsync(entity);
		return await GetItemWithIncludesAsync(entity);
	}

	public async Task DeleteAsync(long id)
	{
		await repository.DeleteAsync(id);
	}

	public async Task<RouteDto> GetAsync(long id)
	{
		var test = await GetItemWithIncludesAsync(id);
		return test;
	}

	public async Task<RouteDto> UpdateAsync(UpdateRouteRequest dto)
	{
		var entity = dto.Adapt<Route>();
		entity = await repository.UpdateRelationShips(entity);
		var test = await GetItemWithIncludesAsync(entity);
		return test;
	}

	public async Task<IEnumerable<RouteDto>> GetRoutesAsync(GetRoutesRequest dto)
	{
		var pageData = repository.Items;

		switch (dto.SortType)
		{
			case RoutesSortingType.ByTitle:
				pageData = pageData.OrderBy(x => x.Title);
				break;
			case RoutesSortingType.ByCreating:
				pageData = pageData.OrderBy(x => x.CreatedAt);
				break;
		}

		if (dto.Filters is { })
		{
			bool isVisibleFilter = dto.Filters.Any(x => x == RoutesFiltersType.ShowVisible);
			bool isHiddenFilter = dto.Filters.Any(x => x == RoutesFiltersType.ShowHidden);
			if (isVisibleFilter && !isHiddenFilter)
			{
				pageData = pageData.Where(x => !x.IsHidden);
			}
			else if (!isVisibleFilter && isHiddenFilter)
			{
				pageData = pageData.Where(x => x.IsHidden);
			}
		}

		pageData.Skip((dto.PageNumber - 1) * dto.PageSize)
			.Take(dto.PageSize);

		return pageData.Include(x => x.RouteCategories)
			.ToList()
			.Adapt<IEnumerable<RouteDto>>();
	}

	public async Task HideRoute(long id)
	{
		var entity = await repository.GetAsync(id);
		entity.IsHidden = true;
		await repository.UpdateAsync(entity);
	}

	public async Task ShowRoute(long id)
	{
		var entity = await repository.GetAsync(id);
		entity.IsHidden = false;
		await repository.UpdateAsync(entity);
	}

	private async Task<RouteDto> GetItemWithIncludesAsync(Route route)
	{
		return await GetItemWithIncludesAsync(route.Id);
	}

	private async Task<RouteDto> GetItemWithIncludesAsync(long? id)
	{
		var result = await repository
			.Items
			.Include(x => x.Attachment)
			.Include(x => x.RouteCategories)
			.FirstOrDefaultAsync(x => id == x.Id) ?? throw new InvalidOperationException("Ошибка получения");

		return result.Adapt<RouteDto>();
	}

	public async Task<IEnumerable<RouteDto>> GetVisibleRoutesAsync(GetVisibleRoutesRequest request)
	{
		var routesQuery = repository.Items.Where(x => x.IsHidden == false).Include(x => x.Attachment).Include(x => x.RouteCategories).AsQueryable();

		if (request.Category is { })
		{
			routesQuery = routesQuery.Where(x => x.RouteCategories.Any(rc => rc.Title == request.Category));
		}


		if (request.PageNumber is { } && request.PageSize is { })
		{
			routesQuery.Paginate(request.PageNumber.Value, request.PageSize.Value);
		}


		var result = await routesQuery.ToListAsync();
		return result.Adapt<IEnumerable<RouteDto>>();
	}
}
