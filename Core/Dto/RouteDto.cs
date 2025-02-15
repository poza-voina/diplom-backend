using System.Text.Json.Serialization;
using Core.Entities;

namespace Core.Dto;

public class RouteDto
{
	public static readonly string URI_PATTERN_ADMIN = "admin/route/{0}";

	#region
	[JsonPropertyName("id")]
	public long? Id { get; set; }

	[JsonPropertyName("title")]
	public string Title { get; set; }

	[JsonPropertyName("description")]
	public string Description { get; set; }

	[JsonPropertyName("maxCountPeople")]
	public int MaxCountPeople { get; set; }

	[JsonPropertyName("minCountPeople")]
	public int MinCountPeople { get; set; }

	[JsonPropertyName("baseCost")]
	public double BaseCost { get; set; }

	[JsonPropertyName("creationDateTime")]
	public DateTime CreationDateTime { get; set; }

	[JsonPropertyName("routeTypes")]
	public string RouteTypes { get; set; }

	[JsonPropertyName("isHidden")]
	public bool IsHidden { get; set; }
	#endregion

	public static Route ToEntity(RouteDto dto)
	{
		return new Route
		{
			Title = dto.Title,
			Description = dto.Description,
			MaxCountPeople = dto.MaxCountPeople,
			MinCountPeople = dto.MinCountPeople,
			BaseCost = (float)dto.BaseCost,
			CreationDateTime = dto.CreationDateTime,
			RouteTypes = dto.RouteTypes,
			IsHidden = dto.IsHidden,
			Id = dto.Id
		};
	}

	public static RouteDto FromEntity(Route entity)
	{
		return new RouteDto
		{
			Id = entity.Id,
			Title = entity.Title,
			Description = entity.Description,
			MaxCountPeople = entity.MaxCountPeople,
			MinCountPeople = entity.MinCountPeople,
			BaseCost = entity.BaseCost,
			CreationDateTime = entity.CreationDateTime,
			RouteTypes = entity.RouteTypes,
			IsHidden = entity.IsHidden
		};
	}
}