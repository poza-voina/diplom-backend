using Entities = Infrastructure.Entities;
using Mapster;
using Infrastructure.Entities;

namespace Core.Dto;

public class RouteExampleDto
{
	public long? Id { get; set; }
	public long? RouteId { get; set; }
	public DateTime? CreationDateTime { get; set; }
	public DateTime? StartDateTime { get; set; }
	public DateTime? EndDateTime { get; set; }
	public RouteExampleStatus Status { get; set; }
	public long CountRecords { get; set; }

	public static Entities.RouteExample ToEntity(RouteExampleDto dto)
		=> dto.Adapt<Entities.RouteExample>();

	public static RouteExampleDto FromEntity(Entities.RouteExample entity)
		=> entity.Adapt<RouteExampleDto>();
}