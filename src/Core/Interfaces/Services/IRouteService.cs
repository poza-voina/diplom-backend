using Core.Dto.Route;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;

public interface IRouteService
{
	Task<RouteDto> CreateAsync(CreateRouteRequest request);
	Task DeleteAsync(long id);
	Task<RouteDto> GetAsync(long id);
	Task<RouteDto> UpdateAsync(UpdateRouteRequest request);
	Task HideRoute(long id);
	Task ShowRoute(long id);
	Task<IEnumerable<RouteDto>> GetRoutesAsync(GetRoutesRequest request);
	Task<IEnumerable<RouteDto>> GetVisibleRoutesAsync(GetVisibleRoutesRequest request);
}
