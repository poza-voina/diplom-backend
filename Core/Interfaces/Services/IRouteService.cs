using Core.Dto;
using Core.Entities;
using Core.Interfaces.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService : IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute>, ICrudService<RouteDto, RouteDto>, ICrudService<Route, RouteDto>, ICrudServiceById<RouteDto>
{
	Task HideRoute(RouteDto dto);
	Task ShowRoute(RouteDto dto);
	Task<IEnumerable<RouteDto>> GetPerPage(int pageNumber, int countPerPage);
}
