namespace Core.Dto.RouteCategory;

public class UpdateRouteCategoryRequest
{
	public required string Title { get; set; }
	public string? Description { get; set; }
}
