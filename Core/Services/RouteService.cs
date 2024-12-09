using Core.Entities;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services;

public class RouteService(IRouteRepository repository) : IRouteService
{
	private readonly IRouteRepository _repository = repository;

	public async Task<Route> CreateRoute(Route route)
	{
		return await _repository.CreateAsync(route);
	}

	public async Task DeleteRoute(Route route)
	{
		await _repository.DeleteAsync(route);
	}

	public async Task<Route> GetRoute(long id)
	{
		return await _repository.GetAsync(id);
	}

	public async Task<Route> UpdateRoute(Route route)
	{
		return await _repository.UpdateAsync(route);
	}
}
