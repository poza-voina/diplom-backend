namespace Core.Dto.RouteCategory;

public class UpdateRouteCategoryRequest
{
	public required long Id { get; set; }
	public required string Title { get; set; }
	public string? Description { get; set; }
}
