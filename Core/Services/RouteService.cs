using Core.Dto;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class RouteService(IRouteRepository repository) : IRouteService
{
	private readonly IRouteRepository _repository = repository;

	public async Task<RouteDto> CreateRoute(Route route)
	{
		return RouteDto.FromEntity(await _repository.CreateAsync(route));
	}

	public async Task<RouteDto> CreateRoute(RouteDto route)
	{
		return RouteDto.FromEntity(await _repository.CreateAsync(RouteDto.ToEntity(route)));
	}

	public async Task<IEnumerable<IFilteredRoute>?> GetFilteredValuesAsync(IEnumerable<Func<IQueryable<IFilteredRoute>, IQueryable<IFilteredRoute>>> funcs)
	{
		IQueryable<IFilteredRoute> routes = _repository.Items;
		foreach (var func in funcs)
		{
			routes = func(routes);
		}

		return routes.Cast<Route>().Select(x => RouteDto.FromEntity(x)).AsEnumerable();
	}

	public async Task DeleteRoute(Route route)
	{
		await _repository.DeleteAsync(route);
	}

	public async Task<RouteDto> GetRoute(long id)
	{
		return RouteDto.FromEntity(await _repository.GetAsync(id));
	}

	public async Task<IEnumerable<RouteDto>> GetRoutesPerPage(int pageNumber, int countPerPage)
	{
		var pageData = _repository.Items
					.Skip((pageNumber - 1) * countPerPage)
					.Take(countPerPage);

		return pageData.Select(x => RouteDto.FromEntity(x));
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
