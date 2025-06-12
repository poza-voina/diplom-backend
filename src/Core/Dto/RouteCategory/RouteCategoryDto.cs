namespace Core.Dto.RouteCategory;

public class RouteCategoryDto
{
	public long Id { get; set; }
	public required string Title { get; set; }
	public string? Description { get; set; }
	public int CountRoutes { get; set; }
}
