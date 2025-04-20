using Application.Controllers;

namespace Core.Dto.Route;

public class GetRoutesRequest
{
	public int PageNumber { get; set; }
	public int CountPerPage { get; set; }
	public RoutesSortingType SortType { get; set; }
	public IEnumerable<RoutesFiltersType>? Filters { get; set; }
}