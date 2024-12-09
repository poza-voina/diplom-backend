using Core.Entities;

namespace Core.Dto;

public class CuePointDto
{
	public CuePointDto() { }
	public long Id { get; set; }
	public string? Title { get; set; }
	public string? Description { get; set; }
	public string? Address { get; set; }
	public string? CuePointType { get; set; }
	public DateTime? CreationDateTime { get; set; }
	public long? RouteId { get; set; }
	public int SortIndex { get; set; } = -1;

	public static CuePoint ToEntity(CuePointDto dto)
	{
		return new CuePoint(id: dto.Id,
					title: dto.Title ?? throw new NullReferenceException("The value of 'dto.Title' should not be null"),
					description: dto.Description ?? throw new NullReferenceException("The value of 'dto.Description' should not be null"),
					cuePointType: dto.CuePointType ?? throw new NullReferenceException("The value of 'dto.CuePointType' should not be null"),
					creationDateTime: (dto.CreationDateTime ?? throw new NullReferenceException("The value of 'dto.CreationDateTime' should not be null")),
					routeId: dto.RouteId ?? throw new NullReferenceException("The value of 'dto.routeId' should not be null"),
					sortIndex: dto.SortIndex);
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
			SortIndex = entity.SortIndex
		};
	}
}