using Core.Dto;
using Core.Dto.Route;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService
{
	Task<RouteDto> CreateAsync(CreateRouteRequest dto);
	Task DeleteAsync(long id);
	Task<RouteDto> GetAsync(long id);
	Task<RouteDto> UpdateAsync(UpdateRouteRequest dto);
	Task HideRoute(long id);
	Task ShowRoute(long id);
	Task<IEnumerable<RouteDto>> GetRoutesAsync(GetRoutesRequest dto);
}
