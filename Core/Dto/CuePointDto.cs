using Infrastructure.Entities;
using Mapster;

namespace Core.Dto;

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

	public static CuePoint ToEntity(CuePointDto dto)
	{
		return dto.Adapt<CuePoint>();
	}

	public static CuePointDto FromEntity(CuePoint entity)
	{
		return entity.Adapt<CuePointDto>();
	}
}