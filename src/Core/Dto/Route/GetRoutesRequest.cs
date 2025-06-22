using Application.Controllers;

namespace Core.Dto.Route;

public class GetRoutesRequest : IPaggination
{
	public string? Title { get; set; }
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
	public RoutesSortingType SortType { get; set; }
	public IEnumerable<RoutesFiltersType>? Filters { get; set; }
}