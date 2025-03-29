using System.Linq;
using Application.Controllers;
using Core.Dto;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class RouteService(IRepository<Route> _repository) : IRouteService
{

	public async Task<RouteDto> CreateAsync(RouteDto entity) =>
	await CreateAsync(RouteDto.ToEntity(entity));

	public async Task<RouteDto> CreateAsync(Route entity) =>
			RouteDto.FromEntity(await _repository.CreateAsync(entity));

	public async Task DeleteAsync(RouteDto entity) =>
			await DeleteAsync(RouteDto.ToEntity(entity));

	public async Task DeleteAsync(long id) =>
			await _repository.DeleteAsync(id);

	public async Task DeleteAsync(Route entity) =>
			await _repository.DeleteAsync(entity);

	public async Task<RouteDto> GetAsync(long id) =>
			RouteDto.FromEntity(await _repository.GetAsync(id));

	public async Task<RouteDto> UpdateAsync(RouteDto entity)
	{
		return await UpdateAsync(RouteDto.ToEntity(entity));
	}

	public async Task<RouteDto> UpdateAsync(Route entity)
	{
		return RouteDto.FromEntity(await _repository.UpdateAsync(entity));
	}

	public async Task<RouteDto> CreateRoute(Route route)
	{
		return RouteDto.FromEntity(await _repository.CreateAsync(route));
	}

	public async Task<RouteDto> CreateRoute(RouteDto route)
	{
		return RouteDto.FromEntity(await _repository.CreateAsync(RouteDto.ToEntity(route)));
	}

	public async Task DeleteRoute(Route route)
	{
		await _repository.DeleteAsync(route);
	}

	public async Task<RouteDto> GetRouteAsync(long id) =>
		RouteDto.FromEntity(await _repository.GetAsync(id));

	public async Task<IEnumerable<RouteDto>> GetRoutesAsync(GetRoutesDto dto)
	{
		var pageData = _repository.Items;

		switch(dto.SortType)
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

		return pageData.Select(x => RouteDto.FromEntity(x)).ToList();
	}

	public async Task<RouteDto> UpdateRoute(Route route)
	{
		return RouteDto.FromEntity(await _repository.UpdateAsync(route));
	}

	public async Task HideRoute(RouteDto dto)
	{
		dto.IsHidden = true;
		await _repository.UpdateAsync(RouteDto.ToEntity(dto));
	}

	public async Task ShowRoute(RouteDto dto)
	{
		dto.IsHidden = false;
		var a = await _repository.UpdateAsync(RouteDto.ToEntity(dto));
		var test = 0;
	}
}
