﻿using Core.Dto;
using Core.Dto.RouteExample;
using Infrastructure.Entities;
using Infrastructure.Enums;

namespace Core.Interfaces.Services;

public interface IRouteExampleService
{
	Task<IEnumerable<RouteExampleDto>> GetExamplesByRouteId(long routeId);
	Task<RouteExampleDto> CreateOrUpdateAsync(RouteExampleCreateOrUpdateRequest request);
	Task<IEnumerable<RouteExampleDto>> CreateOrUpdateByRouteAsync(IEnumerable<RouteExampleCreateOrUpdateRequest> request);
	Task<IEnumerable<RouteExampleDto>> GetByMonthAsync(GetRoutesExampleFromMonthRequest request);
	Task<RouteExampleRecordWithRouteExampleDto> BookAsync(Client client, long routeExampleId);
	Task<RouteExampleRecordWithRouteExampleDto> UnBookAsync(Client client, long routeExampleId);
	Task DeleteAsync(long id);
	Task<CollectionDto<RouteExampleWithRouteDto>> GetExamplesFilterAsync(GetFilteredRoutesExamplesRequest request);
	Task<RouteExampleDto> GetAsync(long id);
}