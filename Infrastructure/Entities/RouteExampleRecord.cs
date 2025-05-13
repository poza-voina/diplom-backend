using Infrastructure.Enums;

namespace Infrastructure.Entities;

public class RouteExampleRecord : BaseEntity
{
	public long ClientId { get; set; }
	public long RouteExampleId { get; set; }
	public DateTime CreatedAt { get; set; }
	public RouteExampleRecordStatus Status { get; set; }

	public virtual Client? Client { get; set; }
	public virtual RouteExample? RouteExample { get; set; }
}