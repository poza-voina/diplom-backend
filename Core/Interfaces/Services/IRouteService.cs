using Core.Dto;
using Core.Entities;
using Core.Interfaces.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService : IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute>
{
	public Task<RouteDto> CreateRoute(Route route);
	public Task<RouteDto> CreateRoute(RouteDto route);
	public Task<RouteDto> UpdateRoute(Route route);
	public Task DeleteRoute(Route route);
	public Task<RouteDto> GetRoute(long id);
	public Task<IEnumerable<RouteDto>> GetRoutesPerPage(int pageNumber, int countPerPage);
	public Task HideRoute(RouteDto dto);
	public Task ShowRoute(RouteDto dto);

}
