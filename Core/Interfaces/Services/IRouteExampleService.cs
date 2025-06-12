using Core.Dto;
using Core.Dto.RouteCategory.RouteExample;
using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Core.Interfaces.Services;

public interface IRouteExampleService
{
	Task<IEnumerable<RouteExampleDto>> GetExamplesByRouteId(long routeId);
	Task<RouteExampleDto> CreateOrUpdateAsync(RouteExampleCreateOrUpdateRequest request);
	Task<IEnumerable<RouteExampleDto>> CreateOrUpdateByRouteAsync(IEnumerable<RouteExampleCreateOrUpdateRequest> request);
	Task<IEnumerable<RouteExampleDto>> GetByMonthAsync(GetRoutesExampleFromMonthRequest request);
	Task BookAsync(Client client, long routeExampleId);
	Task UnBookAsync(Client client, long routeExampleId);
	Task DeleteAsync(long id);
	Task<RouteExampleWithRouteDto[]> GetExamplesFilterAsync(GetFilteredRoutesExamplesRequest request);
	Task<RouteExampleDto> GetAsync(long id);
}