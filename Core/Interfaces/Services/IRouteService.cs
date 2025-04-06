using Application.Controllers;
using Core.Dto;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService : ICrudService<RouteDto, RouteDto>, ICrudService<Route, RouteDto>, ICrudServiceById<RouteDto>
{
	Task HideRoute(RouteDto dto);
	Task ShowRoute(RouteDto dto);
	Task<IEnumerable<RouteDto>> GetRoutesAsync(GetRoutesDto dto);
}
