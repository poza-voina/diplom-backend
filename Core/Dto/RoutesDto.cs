using System.Collections.ObjectModel;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Entities;
using Core.Interfaces.Repositories;
using Mapster;

namespace Core.Dto;

public class RoutesDto : IGettableFilteredRoutes<IFilteredRoute, IFilteredRoute>
{
	public List<RouteDto> Values { get; set; }

	public RoutesDto(IEnumerable<RouteDto> values)
	{
		Values = values.ToList();
	}

	public RoutesDto()
	{
		Values = [];
	}

	public static RoutesDto FromEntities(IEnumerable<Route> entities)
	{
		List<RouteDto> result = [];
		foreach (var item in entities)
		{
			RouteDto test = RouteDto.FromEntity(item);
			result.Add(test);
		}
		return new(result);
	}

	public static IEnumerable<Route> ToEntities(RoutesDto dto)
	{
		IEnumerable<Route> result = [];

		foreach (var item in dto.Values)
		{
			result.Append(RouteDto.ToEntity(item));
		}
		return result;
	}

	public async Task<IEnumerable<IFilteredRoute>?> GetFilteredValuesAsync(IEnumerable<Func<IQueryable<IFilteredRoute>, IQueryable<IFilteredRoute>>> funcs)
	{
		IQueryable<IFilteredRoute> routes = Values.AsQueryable<IFilteredRoute>();
		foreach (var func in funcs)
		{
			routes = func(routes);
		}

		return routes;
	}
}
