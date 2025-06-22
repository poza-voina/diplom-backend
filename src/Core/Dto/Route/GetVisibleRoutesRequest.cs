namespace Core.Dto.Route;

public class GetVisibleRoutesRequest : IPaggination
{
	public string? Title {get;set;}
    public int? PageNumber { get; set; }
	public int? PageSize { get; set; }
	public string? Category { get; set; }
}
