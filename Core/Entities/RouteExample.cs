using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class RouteExample : BaseEntity
{
	public RouteExample(long id, long routeId, DateTime creationDateTime, DateTime startDateTime, DateTime endDateTime) : base(id)
	{
		RouteId = routeId;
		CreationDateTime = creationDateTime;
		StartDateTime = startDateTime;
		EndDateTime = endDateTime;
	}

	[Column("RouteId")]
	public long RouteId { get; set; }
	
	[Column("CreationDateTime")]
	public DateTime CreationDateTime { get; set; }

	[Column("StartDateTime")]
	public DateTime StartDateTime { get; set; }

	[Column("EndDateTime")]
	public DateTime EndDateTime { get; set; }

	public virtual Route? Route { get; set; }
}
