using Infrastructure.Entities;
using Mapster;

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

	public static RouteExample ToEntity(RouteExampleDto dto) => dto.Adapt<RouteExample>();

	public static RouteExampleDto FromEntity(RouteExample entity) => entity.Adapt<RouteExampleDto>();
}