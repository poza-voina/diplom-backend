using Core.Dto;
using Core.Dto.RouteCategory;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class RouteCategoryService(IRepository<RouteCategory> repository, IRepository<Route> routeRepository) : IRouteCategoryService
{
	private static readonly TypeAdapterConfig _mapsterConfig = GetMapsterConfig();

	public async Task<RouteCategoryDto> CreateAsync(CreateRouteCategoryRequest dto)
	{
		var result = await repository.CreateAsync(dto.Adapt<RouteCategory>());
		return result.Adapt<RouteCategoryDto>(_mapsterConfig);
	}

	public async Task DeleteAsync(long id)
	{
		await repository.DeleteAsync(id);
	}

	public async Task<IEnumerable<RouteCategoryDto>> FilterAsync(FilterRouteCategoryRequest dto)
	{
		var result = await repository
			.Items
			.Include(x => x.Routes)
			.Paginate(dto.PageNumber, dto.PageSize)
			.ToListAsync();

		return result.Adapt<IEnumerable<RouteCategoryDto>>(_mapsterConfig);
	}

	public async Task<RouteCategoryDto> GetAsync(long id)
	{
		var result = await repository
			.Items
			.FirstOrDefaultAsync(x => x.Id == id) ?? throw new EntityNotFoundException("Не удалось найти категорию");

		return result.Adapt<RouteCategoryDto>(_mapsterConfig);
	}

	public async Task<RouteCategoryDto> UpdateAsync(UpdateRouteCategoryRequest dto)
	{
		var result = await repository.UpdateAsync(dto.Adapt<RouteCategory>());
		return result.Adapt<RouteCategoryDto>(_mapsterConfig);
	}

	private static TypeAdapterConfig GetMapsterConfig()
	{
		var mapsterConfig = new TypeAdapterConfig();

		mapsterConfig.NewConfig<RouteCategory, RouteCategoryDto>()
			.Map(dest => dest.CountRoutes, src => src.Routes.Count);

		return mapsterConfig;
	}

	public async Task<IEnumerable<RouteCategoryDto>> GetByRouteIdAsync(long id)
	{
		var route = await routeRepository
			.Items
			.Include(x => x.RouteCategories)
			.FirstOrDefaultAsync() ?? throw new EntityNotFoundException("Маршрут не найден");

		return route.RouteCategories.Adapt<IEnumerable<RouteCategoryDto>>();

	}

	public async Task<IEnumerable<RouteCategoryDto>> GetAllAsync()
	{
		var entities = await repository
			.Items
			.Include(x => x.Routes)
			.ToListAsync();

		return entities.Adapt<IEnumerable<RouteCategoryDto>>(_mapsterConfig);
	}
}
