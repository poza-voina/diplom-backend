using Core.Entities;

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
	public double? Latitude { get; set; }
	public double? Longitude { get; set; }

	public static CuePoint ToEntity(CuePointDto dto)
	{
		return new CuePoint
		{
			Id = dto.Id,
			Title = dto.Title ?? throw new NullReferenceException("The value of 'dto.Title' should not be null"),
			Description = dto.Description,
			CuePointType = dto.CuePointType,
			CreationDateTime = dto.CreationDateTime,
			RouteId = dto.RouteId ?? throw new NullReferenceException("The value of 'dto.RouteId' should not be null"),
			SortIndex = dto.SortIndex,
			Latitude = dto.Latitude,
			Longitude = dto.Longitude
		};
	}

	public static CuePointDto FromEntity(CuePoint entity)
	{
		return new CuePointDto
		{
			Id = entity.Id,
			Title = entity.Title,
			Description = entity.Description,
			CuePointType = entity.CuePointType,
			CreationDateTime = entity.CreationDateTime,
			RouteId = entity.RouteId,
			SortIndex = entity.SortIndex,
			Latitude = entity.Latitude,
			Longitude = entity.Longitude,
		};
	}
}