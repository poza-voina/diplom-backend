namespace Core.Dto.RouteCategory;

public class RouteCategoryDto
{
	public required string Title { get; set; }
	public string? Description { get; set; }

	public RouteDto? Route { get; set; }
}
