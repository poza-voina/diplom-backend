namespace Core.Dto.RouteCategory;

public class CreateRouteCategoryRequest
{
	public required string Title { get; set; }
	public string? Description { get; set; }
}
