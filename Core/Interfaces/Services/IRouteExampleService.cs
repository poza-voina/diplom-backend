using Core.Dto;
using Infrastructure.Entities;

namespace Core.Interfaces.Services;

public interface IRouteExampleService : ICrudService<RouteExampleDto, RouteExampleDto>, ICrudService<RouteExample, RouteExampleDto>, ICrudServiceById<RouteExampleDto>
{
	Task<IEnumerable<RouteExampleDto>> GetExamplesByRouteId(long routeId);
}
