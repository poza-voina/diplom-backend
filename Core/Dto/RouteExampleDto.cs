using Core.Entities;

namespace Core.Dto;

public class RouteExampleDto
{
	public long? RouteId { get; set; }
	public DateTime? CreationDateTime { get; set; }
	public DateTime? StartDateTime { get; set; }
	public DateTime? EndDateTime { get; set; }

	public static RouteExample ToEntity(RouteExampleDto dto) =>
		new RouteExample(id: default,
				routeId: dto.RouteId ?? throw new NullReferenceException("The value of 'dto.RouteId' should not be null"),
				creationDateTime: dto.CreationDateTime ?? throw new NullReferenceException("The value of 'dto.CreationDateTime' should not be null"),
				startDateTime: dto.StartDateTime ?? throw new NullReferenceException("The value of 'dto.StartDateTime' should not be null"),
				endDateTime: dto.EndDateTime ?? throw new NullReferenceException("The value of 'dto.EndDateTime' should not be null"));

	public static RouteExampleDto FromEntity(RouteExample entity) =>
		new RouteExampleDto
		{
			RouteId = entity.RouteId,
			CreationDateTime = entity.CreationDateTime,
			StartDateTime = entity.StartDateTime,
			EndDateTime = entity.EndDateTime
		};
}