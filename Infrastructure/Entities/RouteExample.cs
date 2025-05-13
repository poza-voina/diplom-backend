using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class RouteExample : BaseEntity
{
	public required long RouteId { get; set; }	
	public required DateTime CreatedAt { get; set; }
	public required DateTime StartDateTime { get; set; }
	public required DateTime EndDateTime { get; set; }

	public virtual Route? Route { get; set; }
	public virtual ICollection<RouteExampleRecord> RouteExampleRecords { get; set; } = [];
}
