using Core.Dto;
using Core.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService
{
	public Task<RouteDto> CreateRoute(Route route);
	public Task<RouteDto> UpdateRoute(Route route);
	public Task DeleteRoute(Route route);
	public Task<RouteDto> GetRoute(long id);
	public Task<RoutesDto> GetRoutesPerPage(int pageNumber, int countPerPage);

	public Task HideRoute(RouteDto dto);
	public Task ShowRoute(RouteDto dto);
}
