namespace Core.Dto.Route;

public class GetVisibleRoutesRequest
{
	public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
	public string? Category { get; set; }
}
