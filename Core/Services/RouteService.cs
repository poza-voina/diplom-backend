using Application.Controllers;
using Core.Dto;
using Core.Dto.Route;
using Core.Dto.RouteCategory;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class RouteService(IRouteRepository _repository) : IRouteService
{
	public async Task<RouteDto> CreateAsync(CreateRouteRequest dto)
	{
		var entity = dto.Adapt<Route>();
		entity = await _repository.CreateAsync(entity);
		return await GetItemWithIncludesAsync(entity);
	}

	public async Task DeleteAsync(long id)
	{
		await _repository.DeleteAsync(id);
	}

	public async Task<RouteDto> GetAsync(long id)
	{
		var test = await GetItemWithIncludesAsync(id);
		return test;
	}

	public async Task<RouteDto> UpdateAsync(UpdateRouteRequest dto)
	{
		var entity = dto.Adapt<Route>();
		entity = await _repository.UpdateRelationShips(entity);
		var test = await GetItemWithIncludesAsync(entity);
		return test;
	}

	public async Task<IEnumerable<RouteDto>> GetRoutesAsync(GetRoutesRequest dto)
	{
		var pageData = _repository.Items;

		switch (dto.SortType)
		{
			case RoutesSortingType.ByTitle:
				pageData = pageData.OrderBy(x => x.Title);
				break;
			case RoutesSortingType.ByCreating:
				pageData = pageData.OrderBy(x => x.CreationDateTime);
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

		pageData.Skip((dto.PageNumber - 1) * dto.CountPerPage)
			.Take(dto.CountPerPage);

		return pageData.Include(x => x.RouteCategories)
			.ToList()
			.Adapt<IEnumerable<RouteDto>>();
	}

	public async Task HideRoute(long id)
	{
		var entity = await _repository.GetAsync(id);
		entity.IsHidden = true;
		await _repository.UpdateAsync(entity);
	}

	public async Task ShowRoute(long id)
	{
		var entity = await _repository.GetAsync(id);
		entity.IsHidden = false;
		await _repository.UpdateAsync(entity);
	}

	private async Task<RouteDto> GetItemWithIncludesAsync(Route route)
	{
		return await GetItemWithIncludesAsync(route.Id);
	}

	private async Task<RouteDto> GetItemWithIncludesAsync(long? id)
	{
		var result = await _repository
			.Items
			.Include(x => x.RouteCategories)
			.FirstOrDefaultAsync(x => id == x.Id) ?? throw new InvalidOperationException("Ошибка получения");

		return result.Adapt<RouteDto>();
	}

	//private static TypeAdapterConfig GetMapsterOutputConfig()
	//{
	//	var mapsterConfig = new TypeAdapterConfig();

	//	mapsterConfig
	//		.NewConfig<Route, RouteDto>()
	//		.Map(dest => dest.Categories,
	//			 src => src.RouteCategories.Select(rc => rc.Adapt<RouteCategory, RouteCategoryDto>(GetCategoriesMapsterConfig())));

	//	return mapsterConfig;
	//}

	//private static TypeAdapterConfig GetMapsterUpdateConfig()
	//{
	//	var mapsterConfig = new TypeAdapterConfig();

	//	mapsterConfig
	//		.NewConfig<UpdateRouteRequest, Route>()
	//		.Map(dest => dest.Categories,
	//			 src => src.RouteCategories.Select(rc => rc.Adapt<RouteCategory, RouteCategoryDto>(GetCategoriesMapsterConfig())));

	//	return mapsterConfig;
	//}

	//private static TypeAdapterConfig GetCategoriesMapsterConfig()
	//{
	//	var mapsterConfig = new TypeAdapterConfig();

	//	mapsterConfig.NewConfig<RouteCategory, RouteCategoryDto>()
	//		.Map(dest => dest.CountRoutes, src => 0);

	//	return mapsterConfig;
	//}
}
