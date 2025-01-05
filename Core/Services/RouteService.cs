using Core.Dto;
using Core.Entities;
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

	public async Task DeleteRoute(Route route)
	{
		await _repository.DeleteAsync(route);
	}

	public async Task<RouteDto> GetRoute(long id)
	{
		return RouteDto.FromEntity(await _repository.GetAsync(id));
	}

	public async Task<RoutesDto> GetRoutesPerPage(int pageNumber, int countPerPage)
	{
		var pageData = _repository.Items
					.Skip((pageNumber - 1) * countPerPage)
					.Take(countPerPage)
					.ToList() ?? throw new InvalidOperationException("ошибка");

		return RoutesDto.FromEntities(pageData);
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
