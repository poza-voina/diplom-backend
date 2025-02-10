using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class RouteExample : BaseEntity
{
	[Column("RouteId")]
	public required long RouteId { get; set; }
	
	[Column("CreationDateTime")]
	public required DateTime CreationDateTime { get; set; }

	[Column("StartDateTime")]
	public required DateTime StartDateTime { get; set; }

	[Column("EndDateTime")]
	public required DateTime EndDateTime { get; set; }

	public virtual Route? Route { get; set; }
}
