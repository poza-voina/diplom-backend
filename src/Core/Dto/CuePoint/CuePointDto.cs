using Core.Dto.Attachment;
using Mapster;
using Entities = Infrastructure.Entities;

namespace Core.Dto.CuePoint;

public class CuePointDto
{
	public CuePointDto() { }
	public long? Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? CuePointType { get; set; }
	public DateTime? CreationDateTime { get; set; }
	public long? RouteId { get; set; }
	public int SortIndex { get; set; } = -1;
	public string? Address { get; set; }
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }
	public AttachmentDto? Attachment { get; set; }

	public static Entities.CuePoint ToEntity(CuePointDto dto)
		=> dto.Adapt<Entities.CuePoint>();

	public static CuePointDto FromEntity(Entities.CuePoint entity)
		=> entity.Adapt<CuePointDto>();
}