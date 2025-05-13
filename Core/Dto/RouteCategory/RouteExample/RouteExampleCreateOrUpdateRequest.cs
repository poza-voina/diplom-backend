namespace Core.Dto.RouteCategory.RouteExample;

public class RouteExampleCreateOrUpdateRequest
{
	public long? Id { get; set; }
	public long RouteId { get; set; }
	public DateTime StartDateTime { get; set; }
	public DateTime EndDateTime { get; set; }
}
