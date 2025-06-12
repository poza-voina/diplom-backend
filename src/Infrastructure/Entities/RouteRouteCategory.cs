namespace Infrastructure.Entities;

public class RouteRouteCategory
{
	public long RouteId { get; set; }
	public Route? Route { get; set; }
	public long RouteCategoryId { get; set; }
	public RouteCategory? RouteCategory { get; set; }
}
