using Core.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService
{
	public Task<Route> CreateRoute(Route route);
	public Task<Route> UpdateRoute(Route route);
	public Task DeleteRoute(Route route);
	public Task<Route> GetRoute(long id);
}
