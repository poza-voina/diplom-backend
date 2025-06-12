namespace Infrastructure.Entities;

public class Attachment : BaseEntity
{
	public required string FileName { get; set; }
	public required string Uri { get; set; }
	public DateTime CreatedAt { get; set; }

	public long? RouteId { get; set; }
	public Route? Route { get; set; }

	public long? CuePointId { get; set; }
	public CuePoint? CuePoint { get; set; }
}