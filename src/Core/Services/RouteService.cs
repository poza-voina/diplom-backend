using Application.Controllers;
using Core.Dto;
using Core.Dto.Route;
using Core.Dto.RouteCategory;
using Core.Exceptions;
using Core.Extensions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Humanizer;
using Infrastructure.Entities;
using Infrastructure.Exceptions;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        var original = await repository.GetAsync(dto.Id);
        var entity = dto.Adapt<Route>();

		entity.CreatedAt = original.CreatedAt;
		entity = await repository.UpdateRoute(entity);

		var test = await GetItemWithIncludesAsync(entity);
		return test;
	}

	public async Task<CollectionDto<RouteDto>> GetRoutesAsync(GetRoutesRequest dto)
	{
        ApiValidationException.ThrowIfPagginateIncorrect(dto);

		var pageData = repository.Items.Include(x => x.RouteCategories).AsQueryable();

		switch (dto.SortType)
		{
			case RoutesSortingType.ByTitle:
				pageData = pageData.OrderBy(x => x.Title);
				break;
			case RoutesSortingType.ByCreating:
				pageData = pageData.OrderBy(x => x.CreatedAt);
				break;
		}

		if (!string.IsNullOrEmpty(dto.Title))
		{
            string title = System.Web.HttpUtility.UrlDecode(dto.Title).ToLower();
            pageData = pageData.Where(x => x.Title != null && x.Title.ToLower().Contains(title));
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

		var totalsPages = 0;
		if (dto.PageNumber is { } && dto.PageSize is { })
		{
            totalsPages = pageData.GetTotalsPages(dto.PageSize.Value);
            pageData = pageData.Paginate(dto.PageNumber.Value, dto.PageSize.Value);
        }

        var resultData = await pageData
			.ToListAsync();

		return new CollectionDto<RouteDto>
		{
			Values = resultData.Adapt<IEnumerable<RouteDto>>(),
			TotalPages = totalsPages
		};
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
			.FirstOrDefaultAsync(x => id == x.Id) ?? throw new EntityNotFoundException("Ошибка получения");

		return result.Adapt<RouteDto>();
	}

	public async Task<CollectionDto<RouteDtoWithExamplesMarker>> GetVisibleRoutesAsync(GetVisibleRoutesRequest request)
	{
		var routesQuery = repository
			.Items
			.Where(x => x.IsHidden == false)
			.Include(x => x.Attachment)
			.Include(x => x.RouteCategories)
			.Include(x => x.RouteExamples)
			.AsQueryable();

		if (request.Category is { })
		{
			routesQuery = routesQuery.Where(x => x.RouteCategories.Any(rc => rc.Title == request.Category));
		}

        if (!string.IsNullOrEmpty(request.Title))
        {
            string title = System.Web.HttpUtility.UrlDecode(request.Title).ToLower();
            routesQuery = routesQuery.Where(x => x.Title != null && x.Title.ToLower().Contains(title));
        }

        routesQuery = routesQuery.OrderByDescending(x => x.RouteExamples.Any(x => x.Status == RouteExampleStatus.Pending));

        var totalPages = 0;
		if (request.PageNumber is { } && request.PageSize is { })
		{
            totalPages = routesQuery.GetTotalsPages(request.PageSize.Value);
            routesQuery = routesQuery.Paginate(request.PageNumber.Value, request.PageSize.Value);
        }

		var routes = await routesQuery.ToListAsync();
        var result = routes.Adapt<List<RouteDtoWithExamplesMarker>>();
        for (int i = 0; i < result.Count; i++)
        {
            result[i].IsExamples = routes[i].RouteExamples.Any(x => x.Status == RouteExampleStatus.Pending);
        }

        return new CollectionDto<RouteDtoWithExamplesMarker>
		{
			Values = result,
			TotalPages = totalPages
        };
	}
}
