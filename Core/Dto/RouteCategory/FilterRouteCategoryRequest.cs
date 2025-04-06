namespace Core.Dto.RouteCategory;

public class FilterRouteCategoryRequest
{
	public int PageNumber { get; set; }
	public int CountPerPage { get; set; }
	public required string Title { get; set; }
	public string? Description { get; set; }
}
