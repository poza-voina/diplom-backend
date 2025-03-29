namespace Application.Controllers;

public class GetRoutesDto
{
	public int PageNumber { get; set; }
	public int CountPerPage{ get; set; }
	public RoutesSortingType SortType { get; set; }
	public IEnumerable<RoutesFiltersType>? Filters { get; set; }
}

