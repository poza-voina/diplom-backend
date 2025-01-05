using Core.Entities;
using Mapster;

namespace Core.Dto;

public class RoutesDto
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
}
