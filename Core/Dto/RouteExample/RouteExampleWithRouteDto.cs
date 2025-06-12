namespace Core.Dto;

public class RouteExampleWithRouteDto : RouteExampleDto
{
	public required RouteDto Route { get; set; }
}