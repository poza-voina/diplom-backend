namespace Infrastructure.Entities;

public class RouteCategory : BaseEntity
{
	public required string Title { get; set; }
	public string? Description { get; set; }

	public virtual ICollection<Route> Routes { get; set; } = [];
}
