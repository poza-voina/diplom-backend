using Infrastructure.Enums;

namespace Core.Dto;

public class RouteExampleRecordDto
{
	public long ClientId { get; set; }
	public long RouteExampleId { get; set; }
	public RouteExampleRecordStatus Status { get; set; }
	public required RouteExampleWithRouteDto RouteExample {get;set;}
}

public class RouteExampleWithRouteDto : RouteExampleDto
{
	public required RouteDto Route { get; set; }
}