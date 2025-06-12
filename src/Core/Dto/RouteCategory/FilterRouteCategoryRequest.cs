namespace Core.Dto.RouteCategory;

public class FilterRouteCategoryRequest
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
}
